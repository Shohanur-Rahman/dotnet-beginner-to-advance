using JobFinderWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinderWebApp.DB
{
    public class JobFinderDbContext : DbContext
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options) : base(options)
        {
        }
        public DbSet<DbTodoItem> TodoItems { get; set; } = null!;
    }
}
