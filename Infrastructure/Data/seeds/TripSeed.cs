using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class TripSeed
    {
        public static void SeedTrips(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>().HasData(
                new Trip
                {
                    TripId = 1,
                    DriverId = 1,
                    VehicleId = 1,
                    LokasiAwal = "Jakarta",
                    LokasiAkhir = "Bandung",
                    WaktuMulai = DateTimeOffset.UtcNow.AddHours(-2),
                    WaktuSelesai = DateTimeOffset.UtcNow,
                    Status = "Completed"
                }
            );
        }
    }
}