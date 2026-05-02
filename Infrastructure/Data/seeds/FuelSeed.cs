using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class FuelSeed
    {
        public static void SeedFuels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fuel>().HasData(
                new Fuel
                {
                    Id = 1,
                    VehicleId = 1,
                    DriverId = 1,
                    JmlLiter = 50,
                    HargaLiter = 15000,
                    HargaTotal = 750000,
                    LokasiPengisian = "Jakarta Pusat",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}