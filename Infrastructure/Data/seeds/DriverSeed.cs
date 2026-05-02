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
                    TanggalBerlaku = new DateTimeOffset(new DateTime(2031, 5, 2, 11, 59, 13, 649), new TimeSpan(0, 0, 0, 0, 0)),
                    Status = "Active"
                }
            );
        }
    }
}