using JobFinder.Entities.Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.DB
{
    public class JobFinderDbContext : DbContext
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options) : base(options)
        {
        }
        public DbSet<DbTodoItem> TodoItems { get; set; } = null!;
    }
}
