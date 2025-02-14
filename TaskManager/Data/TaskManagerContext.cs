using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        { }

        public DbSet<Tarea> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
