using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class MaintenanceSeed
    {
        public static void SeedMaintenances(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maintenance>().HasData(
                new Maintenance
                {
                    Id = 1,
                    VehicleId = 1,
                    Tanggal = DateTime.UtcNow,
                    Catatan = "Rutin maintenance check",
                    Odometer = 5000,
                    Biaya = 500000,
                    CreatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}