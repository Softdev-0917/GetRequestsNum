using Microsoft.EntityFrameworkCore;
using MyProject.Models.Domain;

namespace MyProject.Data
{
    public class MyProjectDbContext : DbContext
    {
        public MyProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
