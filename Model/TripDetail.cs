using System.ComponentModel.DataAnnotations.Schema;

namespace fleetops_backend.Models
{
    public class TripDetail
    {
        public int Id { get; set; }

        [Column("trip_id")]
        public required int TripId { get; set; }
        public Trip? Trip { get; set; }

        public int Jarak { get; set; }          // meters or chosen unit
        public int AvgSpeed { get; set; }       // km/h or chosen unit
        public int Waktu { get; set; }          // seconds or chosen unit
        public string? ItemImageUrlPath { get; set; }
        public string? DriverImageUrlPath { get; set; }
    }
}