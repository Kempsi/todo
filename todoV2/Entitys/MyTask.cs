namespace ToDoListApp.Entitys
{
    public class MyTask
    {
        public enum TaskStatus
        {
            toDo,
            inProgress,
            done
        }

        public enum TaskCategory
        {
            personal,
            school,
            work,
            other
        }


        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public TaskCategory Category { get; set; }
        public bool IsFavourite { get; set; }

        public MyTask()
        {

        }

        public MyTask(int iD, string name, string description, DateTime createDate, DateTime deadline, TaskStatus status, TaskCategory category, bool isFavourite)
        {
            ID = iD;
            Name = name;
            Description = description;
            CreateDate = createDate;
            Deadline = deadline;
            Status = status;
            Category = category;
            IsFavourite = isFavourite;
        }
    }
}
