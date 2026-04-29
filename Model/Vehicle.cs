using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace fleetops_backend.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        
        [Column("driver_id")]
        public required int DriverId { get; set; }
        [ForeignKey(nameof(DriverId))]
        public Driver? Driver { get; set; }

        public required string VehicleName {get;set;}
        public required string PLatNomor {get;set;}
        public required int Odometer {get;set;}
        public required string Tipe {get;set;}
        public required string Status {get;set;}
        public required int MaintenanceIntervalKm {get;set;}
        public required int MaintenanceIntervalDay {get;set;}

        public ICollection<Fuel> Fuel { get; set; } = new List<Fuel>();
        public ICollection<Maintenance> Maintenance { get; set; } = new List<Maintenance>();
        public ICollection<Trip> Trip { get; set; } = new List<Trip>();
        public ICollection<Inspection> Inspection { get; set; } = new List<Inspection>();
    } 
}

