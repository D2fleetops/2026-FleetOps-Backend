using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class InspectionSeed
    {
        public static void SeedInspections(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inspection>().HasData(
                new Inspection
                {
                    Id = 1,
                    DriverId = 1,
                    VehicleId = 1,
                    TripId = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.UtcNow
                }
            );
        }
    }
}