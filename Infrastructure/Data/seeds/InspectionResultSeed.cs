using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class InspectionResultSeed
    {
        public static void SeedInspectionResults(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InspectionResult>().HasData(
                new InspectionResult
                {
                    Id = 1,
                    InspectionId = 1,
                    ItemId = 1,
                    PhotoId = 1,
                    Condition = InspectionCondition.Ok,
                    Note = "Brake pads good condition"
                },
                new InspectionResult
                {
                    Id = 2,
                    InspectionId = 1,
                    ItemId = 2,
                    PhotoId = null,
                    Condition = InspectionCondition.Ok,
                    Note = "Tires properly inflated"
                }
            );
        }
    }
}