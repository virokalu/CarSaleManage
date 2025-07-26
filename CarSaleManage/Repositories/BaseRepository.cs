using CarSaleManage.Data;

namespace CarSaleManage.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;
        protected BaseRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
