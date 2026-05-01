using fleetops_backend.Application.DTOs;
using fleetops_backend.Infrastructure.Data;
using fleetops_backend.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace fleetops_backend.Application.Services
{
    public class TripService
    {
        private readonly AppDbContext _context;

        public TripService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Trip?> StartTripAsync(StartTripDTO dto)
        {
            var driverExists = await _context.Drivers.AnyAsync(d => d.DriverId == dto.DriverId);
            if (!driverExists)
            {
                return null;
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == dto.VehicleId);

            if (vehicle == null)
            {
                throw new InvalidOperationException("Vehicle does not exist.");
            }

            if (vehicle.DriverId != dto.DriverId)
            {
                throw new InvalidOperationException("Vehicle does not belong to the selected driver.");
            }

            Point? startPoint = null;
            if (dto.StartLatitude.HasValue && dto.StartLongitude.HasValue)
            {
                startPoint = new Point(dto.StartLongitude.Value, dto.StartLatitude.Value)
                {
                    SRID = 4326
                };
            }

            var trip = new Trip
            {
                DriverId = dto.DriverId,
                VehicleId = dto.VehicleId,
                LokasiAwal = dto.LokasiAwal,
                KordAwal = startPoint,
                WaktuMulai = DateTimeOffset.UtcNow,
                Status = "Ongoing"
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return trip;
        }
    }
}