using Microsoft.AspNetCore.Mvc;
using Products_backend.Data_Transfer_Objects;
using Products_backend.Services;

namespace Products_backend.Controllers
{
    [ApiController]
    [Route("/api/warehouses")]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // Для выпадающего списка на фронте (GET /api/warehouses)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> Get(CancellationToken cancellationToken)
        {
            var products = await _warehouseService.GetAllAsync(cancellationToken);
            return Ok(products);
        }
    }
}
