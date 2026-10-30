using Microsoft.AspNetCore.Mvc;
using Products_backend.Data_Transfer_Objects;
using Products_backend.models;
using Products_backend.Services;

namespace Products_backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;

        public ProductController(IProductService productService, WarehouseService warehouseService )
        {
            _productService = productService;
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product_catabologist_Dto>>> Get(CancellationToken cancellationToken)
        {
            var products = await _productService.GetCatalogAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product_catabologist_Dto>> Get(int Id, CancellationToken cancellationToken)
        {
            var products = await _productService.GetByIdAsync(Id, cancellationToken);

            if (products == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Товар не найден"
                });
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] Product_creation_Dto pcd, CancellationToken cancellationToken)
        {
            var products = await _warehouseService.ExistsAsync(pcd.Warehouse_id,cancellationToken);

            if (!products) return ValidationProblem(ModelState);

            Product_detailed_Dto createdProduct = await _productService.CreateAsync(pcd, cancellationToken);
            return CreatedAtAction("Get", new {id = createdProduct.Id}, createdProduct);
        }
    }
}
