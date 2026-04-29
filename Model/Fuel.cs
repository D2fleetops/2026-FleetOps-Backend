using System.ComponentModel.DataAnnotations.Schema;

namespace fleetops_backend.Models
{
    public class Fuel
    {
        public int Id { get; set; }                    // PK

        [Column("vehicle_id")]
        public int VehicleId { get; set; }             // FK -> Vehicles.VehicleId
        public Vehicle? Vehicle { get; set; }

        [Column("driver_id")]
        public int? DriverId { get; set; }             // FK -> Drivers.DriverId (nullable jika kadang tidak ada)
        public Driver? Driver { get; set; }

        public int JmlLiter { get; set; }
        public int HargaLiter { get; set; }
        public int HargaTotal { get; set; }
        public string? LokasiPengisian { get; set; }
        public string? ImageUrlPath { get; set; }
        public DateTime CreatedAt { get; set; }
    } 
}

