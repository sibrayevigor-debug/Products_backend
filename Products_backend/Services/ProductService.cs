using Microsoft.EntityFrameworkCore;
using Products_backend.Data_Transfer_Objects;
using Products_backend.models;


namespace Products_backend.Services
{
    public interface IProductService
    {    // Для списка в каталоге (GET /api/products)
        Task<IEnumerable<Product_catabologist_Dto>> GetCatalogAsync(CancellationToken cancellationToken);
        // Для детальной записи (GET /api/products/{id})
        Task<Product_detailed_Dto?> GetByIdAsync(int Id, CancellationToken cancellationToken);
        // Для добавления записи (POST /api/products)
        Task<Product_detailed_Dto> CreateAsync(Product_creation_Dto pcd, CancellationToken cancellationToken);
    }


    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Для списка в каталоге (GET /api/products)
        public async Task<IEnumerable<Product_catabologist_Dto>> GetCatalogAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Warehouse)
                .ToListAsync(cancellationToken);

            return products.Select(p => new Product_catabologist_Dto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Availability = p.Availability.ToString(),
                    Warehouse = new WarehouseDto
                    {
                        Id = p.Warehouse.Id,
                        Name = p.Warehouse.Name
                    }
                 });
          }

        // Для детальной записи (GET /api/products/{id})
        public async Task<Product_detailed_Dto?> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var products = await _context.Products
               .Include(p => p.Warehouse)
               .FirstOrDefaultAsync (p => p.Id == Id, cancellationToken);

            // Проверка на верность Id
            if (products == null) return null;

            return new Product_detailed_Dto
            {
                Id = products.Id,
                Name = products.Name,
                Price = products.Price,
                Quantity = products.Quantity,
                Length_mm = products.Length_mm,
                Width_mm = products.Width_mm,
                Height_mm = products.Height_mm,
                Weight_kg = products.Weight_kg,
                Availability = products.Availability.ToString(),
                Warehouse = new WarehouseDto
                {
                    Id = products.Warehouse.Id,
                    Name = products.Warehouse.Name
                }
            };
            }

        // Для добавления записи (POST /api/products)
        public async Task<Product_detailed_Dto> CreateAsync(Product_creation_Dto pcd, CancellationToken cancellationToken)
            {
            // Создание products
            var product = new Product
            {
                Name = pcd.Name,
                Price = pcd.Price,
                Quantity = pcd.Quantity,
                Length_mm = pcd.Length_mm,
                Width_mm = pcd.Width_mm,
                Height_mm = pcd.Height_mm,
                Weight_kg = pcd.Weight_kg,
                Warehouse_id = pcd.Warehouse_id
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Подгружаем данные  склада
            await _context.Entry(product).Reference(p =>  p.Warehouse).LoadAsync(cancellationToken);
            // Возврашение созданого products
            return new Product_detailed_Dto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Length_mm = product.Length_mm,
                Width_mm = product.Width_mm,
                Height_mm = product.Height_mm,
                Weight_kg = product.Weight_kg,
                Availability = product.Availability.ToString(),
                Warehouse = new WarehouseDto
                {
                    Id = product.Warehouse.Id,
                    Name = product.Warehouse.Name
                }
            };
        }
    }
    }

