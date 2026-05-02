namespace fleetops_backend.Application.DTOs
{
	public class StartTripDTO
	{
		public required int DriverId { get; set; }
		public required int VehicleId { get; set; }
		public string? LokasiAwal { get; set; }
		public double? StartLatitude { get; set; }
		public double? StartLongitude { get; set; }
	}
}
