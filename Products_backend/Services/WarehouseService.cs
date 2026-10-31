
using Microsoft.EntityFrameworkCore;
using Products_backend.Data_Transfer_Objects;

namespace Products_backend.Services
{
    public interface IWarehouseService
    {   // Для выпадающего списка на фронте (GET /api/warehouses)
        Task<IEnumerable<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken);
        // Проверка на верность warehouse_id
        Task<bool> ExistsAsync(int Id, CancellationToken cancellationToken);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;

        public WarehouseService(AppDbContext context)
        {
            _context = context;
        }

        // Для выпадающего списка на фронте (GET /api/warehouses)
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

        // Проверка на верность warehouse_id
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Warehouses.AnyAsync(w => w.Id == id, cancellationToken);
        }
    }
}
