using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace fleetops_backend.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        
        [Column("user_id")]
        public required int UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        
        public string? JenisLisensi {get;set;}
        public DateTimeOffset TanggalBerlaku {get;set;}
        public string? Status {get;set;}
        public ICollection<Vehicle> Vehicle { get; set; } = new List<Vehicle>();
        public ICollection<Fuel> Fuel { get; set; } = new List<Fuel>();
        public ICollection<Trip> Trip { get; set; } = new List<Trip>();
        public ICollection<Inspection> Inspection { get; set; } = new List<Inspection>();
    } 
}
