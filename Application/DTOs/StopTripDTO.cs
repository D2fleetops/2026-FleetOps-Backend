namespace fleetops_backend.Application.DTOs
{
    public class StopTripDTO
    {
        public required int DriverId { get; set; }
        public required int VehicleId { get; set; }
        public string? LokasiAkhir { get; set; }
        public double? EndLatitude { get; set; }
        public double? EndLongitude { get; set; }
    }
}
