using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usr> Usrs { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}