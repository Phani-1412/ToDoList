using ToDoList.DTO;
using Microsoft.EntityFrameworkCore;
namespace ToDoList
{
    public class ToDoService : IToDoListService
    {
        private readonly ToDoListDbContext _context;  
        public ToDoService(ToDoListDbContext context)
        {
            _context = context;
        }
        public async Task<ToDoResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = new ToDo
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate,

                //defaults
                Status = ToDo.TaskStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _context.ToDos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ToDoResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                DueDate = entity.DueDate,
                CompletedAt = entity.CompletedAt
            };
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var entity = await _context.ToDos
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                return false;
            }
            _context.ToDos.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ToDoResponseDto>> GetAllTasksAsync()
        {
            var allTasks= await _context.ToDos
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new ToDoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    CompletedAt = t.CompletedAt
                })
                .ToListAsync();
            return allTasks;
        }

        public async Task<int> GetCompletedTasksAsync()
        {
            return await _context.ToDos
                .CountAsync(t => t.Status == ToDo.TaskStatus.Completed);
        }

        public async Task<int> GetPendingTasksAsync()
        {
            return await _context.ToDos
                .CountAsync(t => t.Status == ToDo.TaskStatus.Pending);
        }

        public async Task<ToDoResponseDto> GetTaskByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var task = await _context.ToDos
                .Where(t => t.Id == id)
                .Select(t => new ToDoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    CompletedAt = t.CompletedAt
                })
                .FirstOrDefaultAsync();

            return task;
        }


        public async Task<IEnumerable<ToDoResponseDto>> GetTasksByPriorityAsync(ToDo.TaskPriority priority)
        {
            return await _context.ToDos
                .Where(t => t.Priority == priority)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new ToDoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    CompletedAt = t.CompletedAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoResponseDto>> GetTasksByStatusAsync(ToDo.TaskStatus status)
        {
            return await _context.ToDos
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new ToDoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    CompletedAt = t.CompletedAt
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalTasksAsync()
        {
            return await _context.ToDos.CountAsync();
        }

        public async Task<ToDoResponseDto> UpdateTaskAsync(int id, UpdateToDoDto dto)
        {
            if (id <= 0 || dto == null)
            {
                return null;
            }

            var entity = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                return null;
            }
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Priority = dto.Priority;
            entity.DueDate = dto.DueDate;

            await _context.SaveChangesAsync();

            return new ToDoResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                DueDate = entity.DueDate,
                CompletedAt = entity.CompletedAt
            };
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, UpdateStatusDto dto)
        {
            if (id <= 0 || dto == null)
            {
                return false;
            }

            var entity = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                return false;
            }

            entity.Status = dto.Status;
            if (dto.Status == ToDo.TaskStatus.Completed)
            {
                entity.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                entity.CompletedAt = null;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
