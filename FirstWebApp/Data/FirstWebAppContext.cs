using FirstWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Data
{
    public class FirstWebAppContext : DbContext
    {
        public FirstWebAppContext (DbContextOptions<FirstWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<FirstWebApp.Models.Student> Student { get; set; } = default!;
        public DbSet<FirstWebApp.Models.Employee> Employee { get; set; } = default!;
        public DbSet<DbUser> Users { get; set; } = default!;
    }
}
