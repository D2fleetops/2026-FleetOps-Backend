namespace fleetops_backend.Application.DTOs
{
    public class StopTripDTO
    {
        public required int DriverId { get; set; }
        public required int VehicleId { get; set; }
        public string? LokasiAkhir { get; set; }
        public double? KordAkhirLatitude { get; set; }
        public double? KordAkhirLongitude { get; set; }
        public double? OdometerAkhir { get; set; }
    }
}
