using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class VehicleSeed
    {
        public static void SeedVehicles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    VehicleId = 1,
                    DriverId = 1,
                    VehicleName = "Truck-001",
                    PLatNomor = "B 1234 ABC",
                    Odometer = 0,
                    Tipe = "Truck",
                    Status = "Active",
                    MaintenanceIntervalKm = 5000,
                    MaintenanceIntervalDay = 90
                }
            );
        }
    }
}