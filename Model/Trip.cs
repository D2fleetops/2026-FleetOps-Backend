using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleetops_backend.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        [Column("driver_id")]
        public required int DriverId { get; set; }
        public Driver? Driver { get; set; }

        [Column("vehicle_id")]
        public required int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        [Column("kord_awal", TypeName = "geography")]
        public Point? KordAwal { get; set; }

        [Column("kord_akhir", TypeName = "geography")]
        public Point? KordAkhir { get; set; }
        public string? LokasiAwal {get; set;}
        public string? LokasiAkhir {get; set;}

        [Column("odometer_awal")]
        public double? OdometerAwal { get; set; }

        [Column("odometer_akhir")]
        public double? OdometerAkhir { get; set; }

        [Column("jarak_tempuh")]
        public double? JarakTempuh { get; set; }

        [Column("waktu_mulai")]
        public DateTimeOffset WaktuMulai { get; set; }

        [Column("waktu_selesai")]
        public DateTimeOffset? WaktuSelesai { get; set; }

        public string? Catatan { get; set; }

        public string Status { get; set; } = string.Empty;

        public ICollection<TripDetail> Detail { get; set; } = new List<TripDetail>();
    }
}