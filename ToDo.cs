namespace ToDoList
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDeleted { get; set; }
        public enum TaskStatus
        {
            Pending,
            InProgress,
            Completed
        }
        public enum TaskPriority
        {
            Low, Medium, High
        }
    }
}