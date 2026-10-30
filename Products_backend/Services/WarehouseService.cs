
using Microsoft.EntityFrameworkCore;
using Products_backend.Data_Transfer_Objects;

namespace Products_backend.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int Id, CancellationToken cancellationToken);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task<IEnumerable<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Warehouses
                .Select(w => new WarehouseDto
            {
                Id = w.Id,
                Name = w.Name
            })
                .ToListAsync(cancellationToken);
        }
     
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Warehouses.AnyAsync(w => w.Id == id, cancellationToken);
        }
    }
}
