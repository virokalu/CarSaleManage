using CarSaleManage.Data;
using CarSaleManage.Models;
using CarSaleManage.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarSaleManage.Repositories
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context) 
        { 
        }

        public async Task<IEnumerable<Vehicle>> ListAsync()
        {
            return await _context.Vehicle
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicle.AddAsync(vehicle);
        }

        public async Task<Vehicle?> FindByIdAsync(int id)
        {
            return await _context.Vehicle.FindAsync(id);
        }
        public async Task<IEnumerable<Vehicle>> FindbyUserAsync(string id)
        {
            return await _context.Vehicle
                .Where(v => v.AppUserId == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Vehicle>> SerachbyChassisNoAsync(string chassisNo)
        {
            return await _context.Vehicle
                .Where(v => v.ChassisNo != null && v.ChassisNo.Contains(chassisNo))
                .ToListAsync();
        }
        public void Update(Vehicle vehicle)
        {
            _context.Vehicle.Update(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            _context.Vehicle.Remove(vehicle);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle?> FIndByIdWithVehicleAsync(int id)
        {
            return await _context.Vehicle.Include(v=>v.AppUser).FirstOrDefaultAsync(v=>v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> SearchListAsync(string searchString, int? searchInt)
        {
            return await _context.Vehicle
                .Where(v => (v.ChassisNo != null && v.ChassisNo.Contains(searchString)) ||
                            (v.Make !=null && v.Make.Contains(searchString)) ||
                            (v.ModelNo != null && v.ModelNo.Contains(searchString)) ||
                            (v.Classification != null && v.Classification.Contains(searchString)) ||
                            (v.Origin != null && v.Origin.Contains(searchString)) ||
                            (v.UsedCountry != null && v.UsedCountry.Contains(searchString)) ||
                            (v.RegNo != null && v.RegNo.Contains(searchString)) ||
                            (v.FuelSystem != null && v.FuelSystem.Contains(searchString)) ||
                            (searchInt.HasValue &&
                             (v.Year == searchInt.Value ||
                              v.EngineCap == searchInt.Value ||
                              v.MeterReading == searchInt.Value))
                )
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }
    }
}
