using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoListApp.Entitys;
using ToDoListApp.Repos;
using static ToDoListApp.Entitys.MyTask;

namespace todoV2.Windows
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        MyTask task;

        public NewTaskWindow()
        {
            InitializeComponent();
            SetWindowIcon();

            task = new MyTask();

            SetComboBoxes();
            SetDate();
            SetWindowToMiddle();

        }

        #region Window icon

        private void SetWindowIcon()
        {
            this.Icon = new BitmapImage(new Uri("F:\\Visual Studio Enterprise 2022 projektit\\Oman ajan extra tehtäviä\\todoV2\\todoV2\\Icons\\todoIconV2.ico"));
        }

        #endregion Window icon

        private void SetComboBoxes()
        {
            // Sets comboboxes itemsource to Enums
            cb_Category.ItemsSource = Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>().Select(ConverseCategory);
            cb_Status.ItemsSource = Enum.GetValues(typeof(MyTask.TaskStatus)).Cast<MyTask.TaskStatus>().Select(ConverseStatus);

            cb_Category.SelectedIndex = 0;
            cb_Status.SelectedIndex = 0;
        }

        private void SetDate()
        {
            dp_Deadline.SelectedDate = DateTime.Now;
        }

        private void SetWindowToMiddle()
        {
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        // Muuntaa enumin comboboksiin selkokielelle valintaa varten
        private string ConverseCategory(TaskCategory category)
        {
            switch (category)
            {
                case TaskCategory.school:
                    return "School";
                case TaskCategory.personal:
                    return "Personal";
                case TaskCategory.work:
                    return "Work";
                case TaskCategory.other:
                    return "Other";
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }


        // Muuntaa enumin comboboksiin selkokielelle valintaa varten
        private string ConverseStatus(MyTask.TaskStatus status)
        {
            switch (status)
            {
                case MyTask.TaskStatus.toDo:
                    return "To do";
                case MyTask.TaskStatus.inProgress:
                    return "In progress";
                case MyTask.TaskStatus.done:
                    return "Done";

                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        // Reads inputs from textboxes
        private void ReadInputs()
        {
            task.Name = txt_Name.Text;
            task.Description = txt_Description.Text;
            var category = cb_Category.SelectedItem.ToString();
            var status = cb_Status.SelectedItem.ToString();
            var dl = dp_Deadline.SelectedDate;
            task.CreateDate = DateTime.Now;
            task.Deadline = (DateTime)dl;
            bool isFavourite = false;

            if (check_Favourite.IsChecked == true)
            {
                isFavourite = true;
            }

            task.IsFavourite = isFavourite;

            switch (category)
            {
                case "Personal":
                    task.Category = TaskCategory.personal;
                    break;
                case "School":
                    task.Category = TaskCategory.school;
                    break;
                case "Work":
                    task.Category = TaskCategory.work;
                    break;
                case "Other":
                    task.Category = TaskCategory.other;
                    break;
                default:
                    break;
            }

            switch (status)
            {
                case "To do":
                    task.Status = MyTask.TaskStatus.toDo;
                    break;
                case "In progress":
                    task.Status = MyTask.TaskStatus.inProgress;
                    break;
                case "Done":
                    task.Status = MyTask.TaskStatus.done;
                    break;

                default:
                    break;
            }


            DBRepo.InsertTask(task);
            DBRepo.AddTaskPanel(task);

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            MinWidth = ActualWidth;
            MinHeight = ActualHeight;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            ReadInputs();
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Name.Text != string.Empty && txt_Description.Text != string.Empty)
            {
                btn_Save.IsEnabled = true;
            }
            else
            {
                btn_Save.IsEnabled = false;
            }
        }
    }
}
