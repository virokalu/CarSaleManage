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
            return await _context.Vehicle.ToListAsync();
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
    }
}
