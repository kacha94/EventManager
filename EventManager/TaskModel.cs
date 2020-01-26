using System;

namespace EventManager
{
    public class TaskModel
    {
        public TaskModel()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 5);
            IsCompleted = false;
            Create_Date = DateTime.Now;
        }

        public TaskModel(string Id, string Title, string Description, bool IsCompleted, DateTime Create_Date)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.IsCompleted = IsCompleted;
            this.Create_Date = Create_Date;
        }

        public string Id { get; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime Create_Date { get; set; }
    }
}
