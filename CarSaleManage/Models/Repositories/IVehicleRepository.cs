namespace CarSaleManage.Models.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> ListAsync();
        Task AddAsync(Vehicle vehicle);
        Task<Vehicle?> FindByIdAsync(int id);
        Task<IEnumerable<Vehicle>> FindbyUserAsync(string id);
        Task<IEnumerable<Vehicle>> SerachbyChassisNoAsync(string chassisNo);
        void Update(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
