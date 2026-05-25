using Microsoft.AspNetCore.Mvc;
using ToDoList;
using ToDoList.DTO;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoController(IToDoListService service)
        {
            _service = service;
        }

        [HttpGet]
        // GET: api/todo
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        // GET: api/todo/{id}
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }
        [HttpGet("status/{status}")]
        // GET: api/todo/status/{status}
        public async Task<IActionResult> GetTasksByStatus(ToDo.TaskStatus status)
        {
            var tasks = await _service.GetTasksByStatusAsync(status);
            return Ok(tasks);
        }
        [HttpGet("priority/{priority}")]
        // GET: api/todo/priority/{priority}
        public async Task<IActionResult> GetTasksByPriority(ToDo.TaskPriority priority)
        {
            var tasks = await _service.GetTasksByPriorityAsync(priority);
            return Ok(tasks);
        }
        [HttpPost]
        // POST: api/todo
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            if (dto == null)
                return BadRequest();

            var createdTask = await _service.CreateTaskAsync(dto);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }
        [HttpPut("{id}")]
        // PUT: api/todo/{id}
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateToDoDto dto)
        {
            if (dto == null)
                return BadRequest();

            var updatedTask = await _service.UpdateTaskAsync(id, dto);

            if (updatedTask == null)
                return NotFound();

            return Ok(updatedTask);
        }
        [HttpPatch("{id}/status")]
        // PATCH: api/todo/{id}/status
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            if (dto == null)
                return BadRequest();

            var result = await _service.UpdateTaskStatusAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        // DELETE: api/todo/{id}
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _service.DeleteTaskAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
        [HttpGet("dashboard")]
        // GET: api/todo/dashboard
        public async Task<IActionResult> GetDashboard()
        {
            var total = await _service.GetTotalTasksAsync();
            var completed = await _service.GetCompletedTasksAsync();
            var pending = await _service.GetPendingTasksAsync();

            return Ok(new
            {
                TotalTasks = total,
                CompletedTasks = completed,
                PendingTasks = pending
            });
        }
    }
}