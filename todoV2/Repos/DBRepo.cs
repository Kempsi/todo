using MySqlConnector;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ToDoListApp.Entitys;
using todoV2.UserControls;
using static ToDoListApp.Entitys.MyTask;


namespace ToDoListApp.Repos
{
    public static class DBRepo
    {
        private const string localWithDB = "Server=127.0.0.1; Port=3306; User ID=user; Pwd=password; Database=ToDoListDB;";

        #region Collections

        public static ObservableCollection<MyTask> tasks = new ObservableCollection<MyTask>();
        public static ObservableCollection<TaskPanel> todayTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> upcomingTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> pastTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> favouriteTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> toDoTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> inProgressTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> doneTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> allTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> schoolTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> personalTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> workTaskPanels = new ObservableCollection<TaskPanel>();
        public static ObservableCollection<TaskPanel> otherTaskPanels = new ObservableCollection<TaskPanel>();

        #endregion Collections

        #region Populating collections

        public static void PopulateLocal()
        {
            tasks = GetTasks();
        }


        public static void CreateTaskPanels()
        {
            tasks.Clear();
            tasks = GetTasks();
            allTaskPanels.Clear();
            foreach (var task in tasks)
            {
                var panel = new TaskPanel();
                panel.Title = task.Name;
                panel.Description = task.Description;
                panel.Deadline = task.Deadline;
                panel.CreateDate = task.CreateDate;
                panel.ID = task.ID;

                switch (task.Category)
                {
                    case TaskCategory.personal:
                        panel.Category = TaskCategory.personal.ToString();
                        break;
                    case TaskCategory.school:
                        panel.Category = TaskCategory.school.ToString();
                        break;
                    case TaskCategory.work:
                        panel.Category = TaskCategory.work.ToString();
                        break;
                    case TaskCategory.other:
                        panel.Category = TaskCategory.other.ToString();
                        break;
                    default:
                        break;
                }

                switch (task.Status)
                {
                    case MyTask.TaskStatus.toDo:
                        panel.Status = MyTask.TaskStatus.toDo.ToString();
                        panel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                        panel.ProgressImageSource = "../Icons/toDo_Icon.png";
                        break;
                    case MyTask.TaskStatus.inProgress:
                        panel.Status = MyTask.TaskStatus.inProgress.ToString();
                        panel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                        panel.ProgressImageSource = "../Icons/inProgress_Icon.png";
                        break;
                    case MyTask.TaskStatus.done:
                        panel.Status = MyTask.TaskStatus.done.ToString();
                        panel.StatusImageSource = "../Icons/done_Icon.png";
                        panel.ProgressImageSource = "../Icons/done_Icon.png";
                        break;
                    default:
                        break;
                }

                if (task.IsFavourite)
                {
                    panel.IsFavourite = true;
                    panel.FavoriteImageSource = "../Icons/favouriteChecked_Icon.png";
                }

                else
                {
                    panel.IsFavourite = false;
                    panel.FavoriteImageSource = "../Icons/favouriteUnchecked_Icon.png";
                }

                allTaskPanels.Add(panel);

            }

            allTaskPanels = SortCollection(allTaskPanels);
        }

        #endregion Populating collections

        #region Filtering collections

        public static void FilterTodays()
        {
            todayTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Deadline.Date == DateTime.Today && panel.Status != MyTask.TaskStatus.done.ToString())
                {
                    todayTaskPanels.Add(panel);
                }
            }

            todayTaskPanels = SortCollection(todayTaskPanels);

        }

        public static void FilterUpcoming()
        {
            upcomingTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Deadline.Date > DateTime.Today && panel.Status != MyTask.TaskStatus.done.ToString())
                {
                    upcomingTaskPanels.Add(panel);
                }
            }

            upcomingTaskPanels = SortCollection(upcomingTaskPanels);
        }

        public static void FilterPast()
        {
            pastTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Deadline.Date < DateTime.Today)
                {
                    pastTaskPanels.Add(panel);
                }
            }

            pastTaskPanels = SortCollection(pastTaskPanels);
        }

        public static void FilterFavourites()
        {
            favouriteTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.IsFavourite)
                {
                    favouriteTaskPanels.Add(panel);
                }
            }

            favouriteTaskPanels = SortCollection(favouriteTaskPanels);
        }

        public static void FilterToDo()
        {
            toDoTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Status == MyTask.TaskStatus.toDo.ToString())
                {
                    toDoTaskPanels.Add(panel);
                }
            }

            toDoTaskPanels = SortCollection(toDoTaskPanels);

        }

        public static void FilterInProgress()
        {
            inProgressTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Status == MyTask.TaskStatus.inProgress.ToString())
                {
                    inProgressTaskPanels.Add(panel);
                }
            }

            inProgressTaskPanels = SortCollection(inProgressTaskPanels);
        }

        public static void FilterDone()
        {
            doneTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Status == MyTask.TaskStatus.done.ToString())
                {
                    doneTaskPanels.Add(panel);
                    panel.mainButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5BC141"));

                }
            }

            doneTaskPanels = SortCollection(doneTaskPanels);
        }

        public static void FilterSchool()
        {
            schoolTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Category == TaskCategory.school.ToString())
                {
                    schoolTaskPanels.Add(panel);


                }
            }

            schoolTaskPanels = SortCollection(schoolTaskPanels);
        }

        public static void FilterPersonal()
        {
            personalTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Category == TaskCategory.personal.ToString())
                {
                    personalTaskPanels.Add(panel);


                }
            }

            personalTaskPanels = SortCollection(personalTaskPanels); 
        }

        public static void FilterWork()
        {
            workTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Category == TaskCategory.work.ToString())
                {
                    workTaskPanels.Add(panel);


                }
            }

            workTaskPanels = SortCollection(workTaskPanels);
        }

        public static void FilterOther()
        {
            otherTaskPanels.Clear();
            foreach (var panel in allTaskPanels)
            {
                if (panel.Category == TaskCategory.other.ToString())
                {
                    otherTaskPanels.Add(panel);


                }
            }

            otherTaskPanels = SortCollection(otherTaskPanels);
        }

        private static ObservableCollection<TaskPanel> SortCollection(ObservableCollection<TaskPanel> collectionToSort)
        {
            return collectionToSort = new ObservableCollection<TaskPanel>(collectionToSort.OrderByDescending(panel => panel.Status).ThenBy(panel => panel.Deadline));
        }



        #endregion Filtering and populating collections

        #region Modifying collections

        public static void DeleteTaskPanel(MyTask myTask)
        {
            var existingPanel = DBRepo.allTaskPanels.FirstOrDefault(p => p.ID == myTask.ID);

            DBRepo.allTaskPanels.Remove(existingPanel);
        }

        public static void UpdateTaskPanel(MyTask myTask)
        {
            var existingPanel = DBRepo.allTaskPanels.FirstOrDefault(p => p.ID == myTask.ID);

            DBRepo.allTaskPanels.Remove(existingPanel);


            var updatedPanel = new TaskPanel();

            updatedPanel.Title = myTask.Name;
            updatedPanel.Description = myTask.Description;
            updatedPanel.Deadline = myTask.Deadline;
            updatedPanel.CreateDate = myTask.CreateDate;
            updatedPanel.ID = myTask.ID;

            switch (myTask.Category)
            {
                case TaskCategory.personal:
                    updatedPanel.Category = TaskCategory.personal.ToString();
                    break;
                case TaskCategory.school:
                    updatedPanel.Category = TaskCategory.school.ToString();
                    break;
                case TaskCategory.work:
                    updatedPanel.Category = TaskCategory.work.ToString();
                    break;
                case TaskCategory.other:
                    updatedPanel.Category = TaskCategory.other.ToString();
                    break;
                default:
                    break;
            }

            switch (myTask.Status)
            {
                case MyTask.TaskStatus.toDo:
                    updatedPanel.Status = MyTask.TaskStatus.toDo.ToString();
                    updatedPanel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                    updatedPanel.ProgressImageSource = "../Icons/toDo_Icon.png";
                    break;
                case MyTask.TaskStatus.inProgress:
                    updatedPanel.Status = MyTask.TaskStatus.inProgress.ToString();
                    updatedPanel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                    updatedPanel.ProgressImageSource = "../Icons/inProgress_Icon.png";
                    break;
                case MyTask.TaskStatus.done:
                    updatedPanel.Status = MyTask.TaskStatus.done.ToString();
                    updatedPanel.StatusImageSource = "../Icons/done_Icon.png";
                    updatedPanel.ProgressImageSource = "../Icons/done_Icon.png";
                    break;
                default:
                    break;
            }

            if (myTask.IsFavourite)
            {
                updatedPanel.IsFavourite = true;
                updatedPanel.FavoriteImageSource = "../Icons/favouriteChecked_Icon.png";
            }

            else
            {
                updatedPanel.IsFavourite = false;
                updatedPanel.FavoriteImageSource = "../Icons/favouriteUnchecked_Icon.png";
            }

            allTaskPanels.Add(updatedPanel);
        }


        public static void AddTaskPanel(MyTask task)
        {
            var panel = new TaskPanel();
            panel.Title = task.Name;
            panel.Description = task.Description;
            panel.Deadline = task.Deadline;
            panel.CreateDate = task.CreateDate;
            panel.ID = task.ID;

            switch (task.Category)
            {
                case TaskCategory.personal:
                    panel.Category = TaskCategory.personal.ToString();
                    break;
                case TaskCategory.school:
                    panel.Category = TaskCategory.school.ToString();
                    break;
                case TaskCategory.work:
                    panel.Category = TaskCategory.work.ToString();
                    break;
                case TaskCategory.other:
                    panel.Category = TaskCategory.other.ToString();
                    break;
                default:
                    break;
            }

            switch (task.Status)
            {
                case MyTask.TaskStatus.toDo:
                    panel.Status = MyTask.TaskStatus.toDo.ToString();
                    panel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                    panel.ProgressImageSource = "../Icons/toDo_Icon.png";
                    break;
                case MyTask.TaskStatus.inProgress:
                    panel.Status = MyTask.TaskStatus.inProgress.ToString();
                    panel.StatusImageSource = "../Icons/checkMarkUnchecked_Icon.png";
                    panel.ProgressImageSource = "../Icons/inProgress_Icon.png";
                    break;
                case MyTask.TaskStatus.done:
                    panel.Status = MyTask.TaskStatus.done.ToString();
                    panel.StatusImageSource = "../Icons/done_Icon.png";
                    panel.ProgressImageSource = "../Icons/done_Icon.png";
                    break;
                default:
                    break;
            }

            if (task.IsFavourite)
            {
                panel.IsFavourite = true;
                panel.FavoriteImageSource = "../Icons/favouriteChecked_Icon.png";
            }

            else
            {
                panel.IsFavourite = false;
                panel.FavoriteImageSource = "../Icons/favouriteUnchecked_Icon.png";
            }

            allTaskPanels.Add(panel);
        }


        #endregion Modifying collections

        #region SELECT - methods

        public static ObservableCollection<MyTask> GetTasks()
        {

            using (var connection = new MySqlConnection(localWithDB))
            {
                connection.Open();

                string query = "SELECT * FROM Task";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string categoryString = reader.GetString("category");
                        string statusString = reader.GetString("status");

                        TaskCategory categoryEnum = (TaskCategory)Enum.Parse(typeof(TaskCategory), categoryString);
                        MyTask.TaskStatus statusEnum = (MyTask.TaskStatus)Enum.Parse(typeof(MyTask.TaskStatus), statusString);

                        MyTask task = new MyTask(
                            reader.GetInt32("TaskID"),
                            reader.GetString("Name"),
                            reader.GetString("Description"),
                            reader.GetDateTime("CreateDate"),
                            reader.GetDateTime("Deadline"),
                            statusEnum,
                            categoryEnum,
                            reader.GetBoolean("IsFavourite")
                            );

                        tasks.Add(task);
                    }
                }
            }

            return tasks;
        }

        public static MyTask GetMyTask(int taskID)
        {
            MyTask task = null;

            using (var conn = new MySqlConnection(localWithDB))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Task WHERE TaskID = @TaskID", conn);
                cmd.Parameters.AddWithValue("@TaskID", taskID);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string categoryValue = reader.GetString("Category");
                        string statusValue = reader.GetString("Status");

                        TaskCategory categoryEnum = (TaskCategory)Enum.Parse(typeof(TaskCategory), categoryValue);
                        MyTask.TaskStatus statusEnum = (MyTask.TaskStatus)Enum.Parse(typeof(MyTask.TaskStatus), statusValue);

                        task = new MyTask(
                            reader.GetInt32("TaskID"),
                            reader.GetString("Name"),
                            reader.GetString("Description"),
                            reader.GetDateTime("CreateDate"),
                            reader.GetDateTime("Deadline"),
                            statusEnum,
                            categoryEnum,
                            reader.GetBoolean("IsFavourite")

                        );
                    }
                }
            }

            return task;
        }

        #endregion SELECT - methods

        #region INSERT - methods

        public static void InsertTask(MyTask task)
        {
            using (var conn = new MySqlConnection(localWithDB))
            {
                conn.Open();

                var sql = "INSERT INTO Task (TaskID, Name, Description, Category, IsFavourite, Status, Deadline, CreateDate)" +
                "VALUES (@TaskID, @Name, @Description, @Category, @IsFavourite , @Status, @Deadline, @CreateDate);";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TaskID", task.ID);
                    cmd.Parameters.AddWithValue("@Name", task.Name);
                    cmd.Parameters.AddWithValue("@Description", task.Description);
                    cmd.Parameters.AddWithValue("@Category", task.Category.ToString());
                    cmd.Parameters.AddWithValue("@IsFavourite", task.IsFavourite);
                    cmd.Parameters.AddWithValue("@Status", task.Status.ToString());
                    cmd.Parameters.AddWithValue("@Deadline", task.Deadline);
                    cmd.Parameters.AddWithValue("@CreateDate", task.CreateDate);

                    cmd.ExecuteNonQuery();
                }

                // Lisätään käyttäjä myös paikalliseen kokoelmaan.
                // Ensin haetaan id tietokannasta:
                sql = "SELECT MAX(TaskID) FROM Task;";
                var cmd2 = new MySqlCommand(sql, conn);
                int id = 0;
                using (MySqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32("MAX(TaskID)");
                    }
                }

                task.ID = id;

                DBRepo.tasks.Add(task);
            }
        }

        #endregion INSERT - methods

        #region DELETE - methods

        public static void DeleteTask(MyTask task)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDB))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM Task WHERE TaskID = @TaskID", conn);
                cmd.Parameters.AddWithValue("@TaskID", task.ID);
                cmd.ExecuteNonQuery();

                cmd.ExecuteNonQuery();

            }
        }

        #endregion DELETE - methods

        #region UPDATE - methods

        public static void UpdateTask(MyTask task)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDB))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand

                    ("UPDATE Task SET " +

                    "Name=@Name, " +
                    "Description=@Description," +
                    "Category=@Category, " +
                    "IsFavourite=@IsFavourite, " +
                    "Status=@Status, " +
                    "Deadline=@Deadline, " +
                    "CreateDate=@CreateDate " +

                    "WHERE TaskID=@TaskID", conn);

                cmd.Parameters.AddWithValue("@TaskID", task.ID);
                cmd.Parameters.AddWithValue("@Name", task.Name);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@Category", task.Category.ToString());
                cmd.Parameters.AddWithValue("@IsFavourite", task.IsFavourite);
                cmd.Parameters.AddWithValue("@Status", task.Status.ToString());
                cmd.Parameters.AddWithValue("@Deadline", task.Deadline);
                cmd.Parameters.AddWithValue("@CreateDate", task.CreateDate);


                cmd.ExecuteNonQuery();


            }

            // Päivitetään käyttäjä myös paikalliseen kokoelmaan.
            foreach (MyTask t in DBRepo.tasks)
            {
                if (t.ID == task.ID)
                {
                    t.Name = task.Name;
                    t.Description = task.Description;
                    t.Category = task.Category;
                    t.IsFavourite = task.IsFavourite;
                    t.Status = task.Status;
                    t.Deadline = task.Deadline;
                    t.CreateDate = task.CreateDate;
                    break;
                }
            }

        }

        #endregion UPDATE - methods

    }
}
