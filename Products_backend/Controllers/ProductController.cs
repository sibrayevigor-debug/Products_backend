using Microsoft.AspNetCore.Mvc;
using Products_backend.Data_Transfer_Objects;
using Products_backend.Services;

namespace Products_backend.Controllers
{
    
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;

        public ProductController(IProductService productService, IWarehouseService warehouseService )
        {
            _productService = productService;
            _warehouseService = warehouseService;
        }

        // Для списка в каталоге (GET /api/products)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product_catabologist_Dto>>> Get(CancellationToken cancellationToken)
        {
            var products = await _productService.GetCatalogAsync(cancellationToken);
            return Ok(products);
        }

        // Для детальной записи (GET /api/products/{id})
        [HttpGet("{id}")]
        public async Task<ActionResult<Product_catabologist_Dto>> Get(int id, CancellationToken cancellationToken)
        {
            var products = await _productService.GetByIdAsync(id, cancellationToken);

            if (products == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Товар не найден"
                });
            return Ok(products);
        }

        // Для добавления записи (POST /api/products)
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] Product_creation_Dto pcd, CancellationToken cancellationToken)
        {
            var products = await _warehouseService.ExistsAsync(pcd.Warehouse_id,cancellationToken);

            // Проверка на верность warehouse_id
            if (!products)
            {
                ModelState.AddModelError("warehouse_id", $"warehouse_id {pcd.Warehouse_id} does not exist.");
                return ValidationProblem(ModelState);
            }

            Product_detailed_Dto createdProduct = await _productService.CreateAsync(pcd, cancellationToken);
            return CreatedAtAction("Get", new {id = createdProduct.Id}, createdProduct);
        }
    }
}
