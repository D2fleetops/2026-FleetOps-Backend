using fleetops_backend.Application.DTOs;
using fleetops_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fleetops_backend.Presentation.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripsController(TripService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartTrip(StartTripDTO dto)
        {
            try
            {
                var trip = await _tripService.StartTripAsync(dto);

                if (trip == null)
                {
                    return BadRequest(new { message = "Invalid driver or vehicle." });
                }

                return Ok(new
                {
                    message = "Trip started successfully",
                    trip.TripId,
                    trip.DriverId,
                    trip.VehicleId,
                    trip.LokasiAwal,
                    trip.WaktuMulai,
                    trip.Status
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/stop")]
        public async Task<IActionResult> StopTrip(int id, StopTripDTO dto)
        {
            try
            {
                var trip = await _tripService.StopTripAsync(id, dto);

                return Ok(new
                {
                    message = "Trip stopped successfully",
                    trip.TripId,
                    trip.WaktuSelesai,
                    trip.LokasiAkhir,
                    trip.Status
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Database error.", detail = ex.Message });
            }
        }
    }
}