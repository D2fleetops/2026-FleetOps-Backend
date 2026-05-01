using Microsoft.EntityFrameworkCore;
using fleetops_backend.Models;

namespace fleetops_backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Driver> Drivers { get; set; }
        
        public DbSet<Vehicle> Vehicles { get; set; }
        
        public DbSet<Fuel> Fuels { get; set; }
        
        public DbSet<Trip> Trips { get; set; }
        
        public DbSet<TripDetail> TripDetails { get; set; }
        
        public DbSet<Inspection> Inspections { get; set; }
        
        public DbSet<InspectionItem> InspectionItems { get; set; }
        
        public DbSet<InspectionPhoto> InspectionPhotos { get; set; }
        
        public DbSet<InspectionResult> InspectionResults { get; set; }
        
        public DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Point geometry properties for Trip
            modelBuilder.Entity<Trip>()
                .Property(t => t.KordAwal)
                .HasColumnType("geography (Point, 4326)");

            modelBuilder.Entity<Trip>()
                .Property(t => t.KordAkhir)
                .HasColumnType("geography (Point, 4326)");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "FleetManager" },
                new Role { Id = 3, Name = "Driver" },
                new Role { Id = 4, Name = "Employee" },
                new Role { Id = 5, Name = "Unassigned" }
            );
                // Roles -> Users (1 Role -> many Users)
    // Add ICollection<Users> Users in Roles if you want two-way nav
    modelBuilder.Entity<Role>()
        .HasMany(r => r.Users)
        .WithOne(u => u.Role)
        .HasForeignKey(u => u.RoleId)
        .OnDelete(DeleteBehavior.Restrict);

    // Users -> Drivers (one-to-one: Driver is profile of User)
    modelBuilder.Entity<Driver>()
        .HasOne(d => d.User)
        .WithOne() // or .WithOne(u => u.Driver) if Users has navigation
        .HasForeignKey<Driver>(d => d.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Drivers -> Vehicles (1 Driver -> many Vehicles)
    modelBuilder.Entity<Vehicle>()
        .HasOne(v => v.Driver)
        .WithMany(d => d.Vehicle) // add ICollection<Vehicles> Vehicles in Drivers
        .HasForeignKey(v => v.DriverId)
        .OnDelete(DeleteBehavior.Restrict);

    // Vehicles -> Fuels (1 Vehicle -> many Fuels)
    modelBuilder.Entity<Fuel>()
        .HasOne(f => f.Vehicle)
        .WithMany(v => v.Fuel) // add ICollection<Fuels> Fuels in Vehicles
        .HasForeignKey(f => f.VehicleId)
        .OnDelete(DeleteBehavior.Cascade);

    // Fuels -> Drivers (optional link)
    modelBuilder.Entity<Fuel>()
        .HasOne(f => f.Driver)
        .WithMany(d => d.Fuel) // add ICollection<Fuels> Fuels in Drivers if needed
        .HasForeignKey(f => f.DriverId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);

    // Vehicles -> Maintenances (1 Vehicle -> many Maintenances)
    modelBuilder.Entity<Maintenance>()
        .HasOne(m => m.Vehicle)
        .WithMany(v => v.Maintenance) // add ICollection<Maintenances> in Vehicles
        .HasForeignKey(m => m.VehicleId)
        .OnDelete(DeleteBehavior.Cascade);

    // Trips -> Drivers (many Trips -> one Driver)
    modelBuilder.Entity<Trip>()
        .HasOne(t => t.Driver)
        .WithMany(d => d.Trip) // add ICollection<Trips> Trips in Drivers
        .HasForeignKey(t => t.DriverId)
        .OnDelete(DeleteBehavior.Restrict);

    // Trips -> Vehicles (many Trips -> one Vehicle)
    modelBuilder.Entity<Trip>()
        .HasOne(t => t.Vehicle)
        .WithMany(v => v.Trip) // add ICollection<Trips> Trips in Vehicles
        .HasForeignKey(t => t.VehicleId)
        .OnDelete(DeleteBehavior.Restrict);

    // TripDetails -> Trips (1 Trip -> many TripDetails)
    modelBuilder.Entity<TripDetail>()
        .HasOne(td => td.Trip)
        .WithMany(t => t.Detail)
        .HasForeignKey(td => td.TripId)
        .OnDelete(DeleteBehavior.Cascade);

    // Inspections -> Drivers & Vehicles
    modelBuilder.Entity<Inspection>()
        .HasOne(i => i.Driver)
        .WithMany(d => d.Inspection) // add ICollection<Inspections> if desired
        .HasForeignKey(i => i.DriverId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Inspection>()
        .HasOne(i => i.Vehicle)
        .WithMany(v => v.Inspection) // add ICollection<Inspections> in Vehicles
        .HasForeignKey(i => i.VehicleId)
        .OnDelete(DeleteBehavior.Restrict);

    // InspectionPhoto -> Inspections (1 Inspection -> many Photos)
    modelBuilder.Entity<InspectionPhoto>()
        .HasOne(p => p.Inspection)
        .WithMany(i => i.Photos)
        .HasForeignKey(p => p.InspectionId)
        .OnDelete(DeleteBehavior.Cascade);

    // InspectionResult -> Inspections (1 Inspection -> many Results)
    modelBuilder.Entity<InspectionResult>()
        .HasOne(r => r.Inspection)
        .WithMany(i => i.Results)
        .HasForeignKey(r => r.InspectionId)
        .OnDelete(DeleteBehavior.Cascade);

    // InspectionResult -> InspectionItem (many Results -> one Item)
    modelBuilder.Entity<InspectionResult>()
        .HasOne(r => r.Item)
        .WithMany(it => it.Result)
        .HasForeignKey(r => r.ItemId)
        .OnDelete(DeleteBehavior.Restrict);

    // InspectionResult -> InspectionPhoto (optional)
    modelBuilder.Entity<InspectionResult>()
        .HasOne(r => r.Photo)
        .WithMany(p => p.Result)
        .HasForeignKey(r => r.PhotoId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);

    // -- Add indexes or column types here if needed (e.g. geography Point setup) --
        }
    }
}