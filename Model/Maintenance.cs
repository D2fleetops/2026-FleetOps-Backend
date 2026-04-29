using System.ComponentModel.DataAnnotations.Schema;

namespace fleetops_backend.Models
{
    public class Maintenance
    {
        public int Id { get; set; }                    // PK

        [Column("vehicle_id")]
        public required int VehicleId { get; set; }             // FK -> Vehicles.VehiclesId
        public Vehicle? Vehicle { get; set; }

        public DateTime Tanggal { get; set; }
        public string Catatan { get; set; } = string.Empty;
        public int? Odometer { get; set; }             // optional
        public decimal? Biaya { get; set; }            // decimal untuk uang
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}