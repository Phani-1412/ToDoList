using ToDoList.DTO;

namespace ToDoList
{
    public interface IToDoListService
    {
        // Create Task
        Task<ToDoResponseDto> CreateTaskAsync(CreateTaskDto dto);

        // Get All Tasks
        Task<IEnumerable<ToDoResponseDto>> GetAllTasksAsync();

        // Get Task by Id
        Task<ToDoResponseDto> GetTaskByIdAsync(int id);

        // Update Task (excluding status)
        Task<ToDoResponseDto> UpdateTaskAsync(int id, UpdateToDoDto dto);

        // Update Status
        Task<bool> UpdateTaskStatusAsync(int id, UpdateStatusDto dto);

        // Soft Delete Task
        Task<bool> DeleteTaskAsync(int id);

        // Filtering
        Task<IEnumerable<ToDoResponseDto>> GetTasksByStatusAsync(ToDo.TaskStatus status);
        Task<IEnumerable<ToDoResponseDto>> GetTasksByPriorityAsync(ToDo.TaskPriority priority);

        // Dashboard
        Task<int> GetTotalTasksAsync();
        Task<int> GetCompletedTasksAsync();
        Task<int> GetPendingTasksAsync();
    }
}
