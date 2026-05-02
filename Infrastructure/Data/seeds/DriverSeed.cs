using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class DriverSeed
    {
        public static void SeedDrivers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>().HasData(
                new Driver 
                { 
                    DriverId = 1,
                    UserId = 1,
                    JenisLisensi = "B",
                    TanggalBerlaku = DateTimeOffset.UtcNow.AddYears(5),
                    Status = "Active"
                }
            );
        }
    }
}