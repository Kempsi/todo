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
using todoV2.UserControls;
using static ToDoListApp.Entitys.MyTask;

namespace todoV2.Windows
{
    /// <summary>
    /// Interaction logic for ModifyTaskWindow.xaml
    /// </summary>
    public partial class ModifyTaskWindow : Window
    {
        TaskPanel taskPanel;
        MyTask myTask;

        public ModifyTaskWindow(TaskPanel panel)
        {
            InitializeComponent();
            SetWindowIcon();

            taskPanel = panel;

            myTask = DBRepo.GetMyTask(taskPanel.ID);


            // Sets comboboxes itemsource to Enums
            cb_Category.ItemsSource = Enum.GetValues(typeof(TaskCategory)).Cast<TaskCategory>().Select(ConverseCategory);
            cb_Status.ItemsSource = Enum.GetValues(typeof(MyTask.TaskStatus)).Cast<MyTask.TaskStatus>().Select(ConverseStatus);

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            SetInfo();



        }

        #region Window icon

        private void SetWindowIcon()
        {
            this.Icon = new BitmapImage(new Uri("F:\\Visual Studio Enterprise 2022 projektit\\Oman ajan extra tehtäviä\\todoV2\\todoV2\\Icons\\todoIconV2.ico"));
        }

        #endregion Window icon

        private void SetComboBoxItem()
        {
            if (myTask.Category == TaskCategory.personal)
            {
                cb_Category.SelectedIndex = 0;
            }

            else if (myTask.Category == TaskCategory.school)
            {
                cb_Category.SelectedIndex = 1;
            }

            else if (myTask.Category == TaskCategory.work)
            {
                cb_Category.SelectedIndex = 2;
            }

            else
            {
                cb_Category.SelectedIndex = 3;
            }



            if (myTask.Status == MyTask.TaskStatus.toDo)
            {
                cb_Status.SelectedIndex = 0;
            }

            else if (myTask.Status == MyTask.TaskStatus.inProgress)
            {
                cb_Status.SelectedIndex = 1;
            }

            else
            {
                cb_Status.SelectedIndex = 2;
            }
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
            myTask.Name = txt_Name.Text;
            myTask.Description = txt_Description.Text;
            var category = cb_Category.SelectedItem.ToString();
            var status = cb_Status.SelectedItem.ToString();
            var dl = dp_Deadline.SelectedDate;
            myTask.CreateDate = DateTime.Now;
            myTask.Deadline = (DateTime)dl;
            bool isFavourite = false;

            if (check_Favourite.IsChecked == true)
            {
                isFavourite = true;
            }

            myTask.IsFavourite = isFavourite;

            switch (category)
            {
                case "Personal":
                    myTask.Category = TaskCategory.personal;
                    break;
                case "School":
                    myTask.Category = TaskCategory.school;
                    break;
                case "Work":
                    myTask.Category = TaskCategory.work;
                    break;
                case "Other":
                    myTask.Category = TaskCategory.other;
                    break;
                default:
                    break;
            }

            switch (status)
            {
                case "To do":
                    myTask.Status = MyTask.TaskStatus.toDo;
                    break;
                case "In progress":
                    myTask.Status = MyTask.TaskStatus.inProgress;
                    break;
                case "Done":
                    myTask.Status = MyTask.TaskStatus.done;
                    break;

                default:
                    break;
            }


            DBRepo.UpdateTask(myTask);
            DBRepo.UpdateTaskPanel(myTask);

        }



        private void SetInfo()
        {
            txt_Name.Text = myTask.Name;
            txt_Description.Text = myTask.Description;
            SetComboBoxItem();
            dp_Deadline.SelectedDate = myTask.Deadline;


            if (myTask.IsFavourite)
            {
                check_Favourite.IsChecked = true;
            }

            else
            {
                check_Favourite.IsChecked = false;
            }

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

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var answer = CustomMessageBox.Show(myTask.Name + "?", "Delete task");

            if (answer)
            {
                DBRepo.DeleteTask(myTask);
                DBRepo.DeleteTaskPanel(myTask);
                Close();
            }


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
