﻿using System.Configuration;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ToDoListApp.Entitys;
using ToDoListApp.Repos;
using todoV2.Windows;

namespace todoV2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private string currentCategory;

        public MainWindow()
        {
            CheckToday();
			InitializeComponent();
            RefreshFrontPage();
            SetWindowIcon();
            

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();

			EncryptAppConfig();

		}

		#region Encrypt AppConfig

		private void EncryptAppConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (!config.ConnectionStrings.SectionInformation.IsProtected)
			{
				config.ConnectionStrings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
				config.Save();
			}
		}

		#endregion Encrypt AppConfig once

		#region Window icon

		private void SetWindowIcon()
        {
			this.Icon = new BitmapImage(new Uri("pack://application:,,,/Icons/todoIconV2.ico"));
		}

        #endregion Window icon

        #region Window size method
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            MinWidth = ActualWidth;
            MinHeight = ActualHeight;
        }

        #endregion Window size method

        #region Refresh methods

        // Shows Today category if there are items with todays deadline
        private void CheckToday()
        {
			currentCategory = "Upcoming";

			var tasks = DBRepo.GetTasks();

            if (tasks.Any(task => task.Deadline.Date == DateTime.Today 
                               && task.Status != MyTask.TaskStatus.done))
            {  
                currentCategory = "Today";
			}

        }

        // Updates current date to the label
        private void UpdateCurrentDate()
        {
            lbl_CurrentDate.Content = DateTime.Now.ToString("f");
        }

        // Timer for updating date
        private void TimerTick(object sender, EventArgs e)
        {
            UpdateCurrentDate();
        }

        // Refreshes all of the UI
        public void RefreshFrontPage()
        {
            DBRepo.CreateTaskPanels();
            DBRepo.FilterFavourites();
            DBRepo.FilterTodays();
            DBRepo.FilterUpcoming();
            DBRepo.FilterPast();
            DBRepo.FilterToDo();
            DBRepo.FilterInProgress();
            DBRepo.FilterDone();
            DBRepo.FilterSchool();
            DBRepo.FilterPersonal();
            DBRepo.FilterWork();
            DBRepo.FilterOther();
            UpdateAmounts();

            UpdateCurrentDate();

            lbl_CurrentDate.Content = DateTime.Now.ToString("f");

            SetCorrectGategory();

        }

        // Sets the correct category depending on current category
        private void SetCorrectGategory()
        {
            listView.Items.Clear();

            switch (currentCategory)
            {
                case "Today":
					lbl_MainTitle.Content = "Today";
					foreach (var panel in DBRepo.todayTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Upcoming":
					lbl_MainTitle.Content = "Upcoming";
					foreach (var panel in DBRepo.upcomingTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Important":
                    foreach (var panel in DBRepo.favouriteTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "All tasks":
                    foreach (var panel in DBRepo.allTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Past":
                    foreach (var panel in DBRepo.pastTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "To do":
                    foreach (var panel in DBRepo.toDoTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "In progress":
                    foreach (var panel in DBRepo.inProgressTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Done":
                    foreach (var panel in DBRepo.doneTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "School":
                    foreach (var panel in DBRepo.schoolTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Personal":
                    foreach (var panel in DBRepo.personalTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Work":
                    foreach (var panel in DBRepo.workTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
                case "Other":
                    foreach (var panel in DBRepo.otherTaskPanels)
                    {
                        listView.Items.Add(panel);
                    }
                    break;
            }
        }

        // Updates the icon amounts for side panel
        private void UpdateAmounts()
        {
            Today.Amount = DBRepo.todayTaskPanels.Count;
            Upcoming.Amount = DBRepo.upcomingTaskPanels.Count;
            Past.Amount = DBRepo.pastTaskPanels.Count;
            Important.Amount = DBRepo.favouriteTaskPanels.Count;
            ToDo.Amount = DBRepo.toDoTaskPanels.Count;
            InProgress.Amount = DBRepo.inProgressTaskPanels.Count;
            Done.Amount = DBRepo.doneTaskPanels.Count;
            AllTasks.Amount = DBRepo.allTaskPanels.Count;
            School.Amount = DBRepo.schoolTaskPanels.Count;
            Personal.Amount = DBRepo.personalTaskPanels.Count;
            Work.Amount = DBRepo.workTaskPanels.Count;
            Other.Amount = DBRepo.otherTaskPanels.Count;
            
        }

		#endregion Refresh methods

		#region Filter buttons

		// Filters for each of the buttons

		private void Today_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Today";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Today";
        }

        private void Upcoming_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Upcoming";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Upcoming";
        }

        private void Important_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Important";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Important";
        }

        private void AllTasks_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "All tasks";
            RefreshFrontPage();

            lbl_MainTitle.Content = "All tasks";
        }

        private void Past_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Past";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Past";
        }

        private void ToDo_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "To do";
            RefreshFrontPage();

            lbl_MainTitle.Content = "To do";
        }

        private void InProgress_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "In progress";
            RefreshFrontPage();

            lbl_MainTitle.Content = "In progress";
        }

        private void Done_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Done";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Done";
        }

        private void School_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "School";
            RefreshFrontPage();

            lbl_MainTitle.Content = "School";
        }

        private void Personal_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Personal";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Personal";
        }

        private void Work_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Work";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Work";
        }

        private void Other_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentCategory = "Other";
            RefreshFrontPage();

            lbl_MainTitle.Content = "Other";
        }

        #endregion Filter buttons

        #region Adding a new task

        private void AddTask_ButtonClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new NewTaskWindow();
            this.Opacity = 0.3;
            addWindow.ShowDialog();
            this.Opacity = 1;


            RefreshFrontPage();
        }



        #endregion Adding a new task

    }
}