namespace ToDoList.DTO
{
    public class ToDoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDo.TaskStatus Status { get; set; }
        public ToDo.TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
