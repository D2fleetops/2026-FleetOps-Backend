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

            // Check if driver already has an active trip
            var activeTrip = await _context.Trips
                .FirstOrDefaultAsync(t => t.DriverId == dto.DriverId && 
                                          (t.Status == "Ongoing" || t.WaktuSelesai == null));
            if (activeTrip != null)
            {
                throw new InvalidOperationException("Driver already has an active trip.");
            }

            // Check if driver has completed inspection for today
            var today = DateTimeOffset.UtcNow.Date;
            var todayInspection = await _context.Set<Inspection>()
                .FirstOrDefaultAsync(i => i.DriverId == dto.DriverId && 
                                          i.CreatedAt.Date == today &&
                                          i.Status == true);
            if (todayInspection == null)
            {
                throw new InvalidOperationException("Driver must complete an inspection before starting a trip.");
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
            if (dto.KordAwalLatitude.HasValue && dto.KordAwalLongitude.HasValue)
            {
                startPoint = new Point(dto.KordAwalLongitude.Value, dto.KordAwalLatitude.Value)
                {
                    SRID = 4326
                };
            }

            var trip = new Trip
            {
                DriverId = dto.DriverId,
                VehicleId = dto.VehicleId,
                LokasiAwal = dto.LokasiAwal,
                LokasiAkhir = dto.LokasiAkhir,
                KordAwal = startPoint,
                OdometerAwal = dto.OdometerAwal,
                WaktuMulai = DateTimeOffset.UtcNow,
                Status = "Ongoing"
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            // Create TripDetail
            var tripDetail = new TripDetail
            {
                TripId = trip.TripId,
                Jarak = 0,
                AvgSpeed = 0,
                Waktu = 0
            };

            _context.Add(tripDetail);
            await _context.SaveChangesAsync();

            return trip;
        }

        public async Task<Trip> StopTripAsync(int tripId, StopTripDTO dto)
        {
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.TripId == tripId);
            if (trip == null)
            {
                throw new KeyNotFoundException("Trip not found.");
            }

            if (trip.DriverId != dto.DriverId)
            {
                throw new InvalidOperationException("Driver does not match the trip.");
            }

            if (trip.VehicleId != dto.VehicleId)
            {
                throw new InvalidOperationException("Vehicle does not match the trip.");
            }

            if (trip.WaktuSelesai.HasValue)
            {
                throw new InvalidOperationException("Trip already finished.");
            }

            if (dto.OdometerAkhir.HasValue && trip.OdometerAwal.HasValue && dto.OdometerAkhir < trip.OdometerAwal)
            {
                throw new InvalidOperationException("Odometer akhir cannot be lower than odometer awal.");
            }

            Point? endPoint = null;
            if (dto.KordAkhirLatitude.HasValue && dto.KordAkhirLongitude.HasValue)
            {
                endPoint = new Point(dto.KordAkhirLongitude.Value, dto.KordAkhirLatitude.Value)
                {
                    SRID = 4326
                };
            }

            trip.KordAkhir = endPoint;
            trip.LokasiAkhir = dto.LokasiAkhir;
            trip.OdometerAkhir = dto.OdometerAkhir;
            trip.WaktuSelesai = DateTimeOffset.UtcNow;
            trip.Status = "Finished";

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();

            // Update TripDetail with calculated metrics
            var tripDetail = await _context.Set<TripDetail>()
                .FirstOrDefaultAsync(td => td.TripId == tripId);

            if (tripDetail != null)
            {
                // Calculate distance traveled from odometer readings
                if (trip.OdometerAwal.HasValue && trip.OdometerAkhir.HasValue)
                {
                    tripDetail.Jarak = (int)(trip.OdometerAkhir.Value - trip.OdometerAwal.Value);
                    trip.JarakTempuh = trip.OdometerAkhir.Value - trip.OdometerAwal.Value;
                    _context.Trips.Update(trip);
                }

                // Calculate trip duration in seconds
                var duration = trip.WaktuSelesai.Value - trip.WaktuMulai;
                tripDetail.Waktu = (int)duration.TotalSeconds;

                // Calculate average speed (distance / time in hours)
                if (tripDetail.Jarak > 0 && duration.TotalSeconds > 0)
                {
                    tripDetail.AvgSpeed = (int)(tripDetail.Jarak / (duration.TotalSeconds / 3600));
                }

                _context.Update(tripDetail);
                await _context.SaveChangesAsync();
            }

            return trip;
        }
    }
}