using Microsoft.EntityFrameworkCore;
namespace ToDoList
{
    public class ToDoListDbContext: DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }
        public DbSet<ToDo> ToDos { get; set; }

    }
}
