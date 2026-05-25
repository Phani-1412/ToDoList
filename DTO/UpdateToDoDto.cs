namespace ToDoList.DTO
{
    public class UpdateToDoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDo.TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
