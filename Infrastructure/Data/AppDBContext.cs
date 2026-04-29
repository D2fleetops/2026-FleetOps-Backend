using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}