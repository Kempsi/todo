using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using todoV2.Windows;

namespace todoV2.UserControls
{
    /// <summary>
    /// Interaction logic for TaskPanel.xaml
    /// </summary>
    public partial class TaskPanel : UserControl, INotifyPropertyChanged
    {
        public TaskPanel()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string currentCategory;

        private string statusImageSource;

        private string favoriteImageSource;

        private string progressImageSource;

        private string title;

        private string category;

        private DateTime deadline;

        private DateTime createDate;

        private int id;

        private string description;

        private string status;

        private bool isFavourite;

        #region Getters and setters

        public bool IsFavourite
        {
            get { return isFavourite; }
            set
            {
                isFavourite = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                OnPropertyChanged();
            }
        }


        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }


        public string ProgressImageSource
        {
            get { return progressImageSource; }
            set
            {
                progressImageSource = value;
                OnPropertyChanged();
            }
        }


        public string FavoriteImageSource
        {
            get { return favoriteImageSource; }
            set
            {
                favoriteImageSource = value;
                OnPropertyChanged();
            }
        }


        public string StatusImageSource
        {
            get { return statusImageSource; }
            set
            {
                statusImageSource = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters and setters


        public event PropertyChangedEventHandler? PropertyChanged;

        public event RoutedEventHandler ButtonClick;



        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string CategoryWithUppercaseFirstLetter
        {
            get => char.ToUpper(Category[0]) + Category.Substring(1);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(this, e);

        }

        private void mainButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.Opacity = 0.3;

            currentCategory = this.category; // Tässä kohtaa otetaan talteen nykyisen avatun taskin kategoria

            var modifyWindow = new ModifyTaskWindow(this);

            modifyWindow.ShowDialog();

            mainWindow.Opacity = 1;



            mainWindow.RefreshFrontPage();    
            
        }

        private void mainButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.Status == "done")
            {
                this.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4DA138"));

            }

            else if (this.Status == "inProgress" || this.Status == "toDo")
            {
                this.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE5E5E5"));
            }

        }

        private void mainButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Status == "done")
            {
                this.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5BC141"));

            }

            else if (this.Status == "inProgress" || this.Status == "toDo")
            {
                this.mainButton.Background = Brushes.White;
            }

        }

        private void mainButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Status == "done")
            {
                this.mainButton.Background = Brushes.Green;

            }

            else if (this.Status == "inProgress" || this.Status == "toDo")
            {
                this.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFACACAC"));
            }
        }

        private void mainButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Status == "done")
            {
                this.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5BC141"));

            }

            else if (this.Status == "inProgress" || this.Status == "toDo")
            {
                this.mainButton.Background = Brushes.White;
            }
        }
    }
}
