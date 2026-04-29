using System.ComponentModel.DataAnnotations.Schema;

namespace fleetops_backend.Models
{
    public class Inspection
    {
        public int Id { get; set; }             // PK (inspect_id)

        [Column("driver_id")]
        public required int DriverId { get; set; }
        public Driver? Driver { get; set; }

        [Column("vehicle_id")]
        public required int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        [Column("trip_id")]
        public required int TripId { get; set; }        // nullable jika inspection tidak selalu terhubung ke trip
        public Trip? Trip { get; set; }

        public required bool Status { get; set; }        // or enum
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<InspectionPhoto> Photos { get; set; } = new List<InspectionPhoto>();
        public ICollection<InspectionResult> Results { get; set; } = new List<InspectionResult>();
    }
}