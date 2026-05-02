using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data.Seeds
{
    public static class InspectionItemSeed
    {
        public static void SeedInspectionItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InspectionItem>().HasData(
                new InspectionItem { Id = 1, Name = "Brake Condition", IsRequired = true },
                new InspectionItem { Id = 2, Name = "Tire Condition", IsRequired = true },
                new InspectionItem { Id = 3, Name = "Lights", IsRequired = true },
                new InspectionItem { Id = 4, Name = "Mirrors", IsRequired = false }
            );
        }
    }
}