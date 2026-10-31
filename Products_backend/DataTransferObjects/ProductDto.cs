using Products_backend.models;
using System.ComponentModel.DataAnnotations;

namespace Products_backend.Data_Transfer_Objects
{
    // Для списка в каталоге (GET /api/products)
    public class Product_catabologist_Dto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Availability { get; set; } = string.Empty;
        public WarehouseDto Warehouse { get; set; } = null!;
    }

    // Для детальной записи (GET /api/products/{id})
    public class Product_detailed_Dto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Length_mm { get; set; }
        public int Width_mm { get; set; }
        public int Height_mm { get; set; }
        public decimal Weight_kg { get; set; }
        public string Availability { get; set; } = string.Empty;
        public WarehouseDto Warehouse { get; set; } = null!;
    }

    // Для добавления записи (POST /api/products)
    public class Product_creation_Dto
    {
 
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Length_mm { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Width_mm { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Height_mm { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Weight_kg { get; set; }

        [Required]
        public int Warehouse_id { get; set; }

    }
}