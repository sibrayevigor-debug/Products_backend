using Products_backend.models;

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
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Length_mm { get; set; }
        public int Width_mm { get; set; }
        public int Height_mm { get; set; }
        public decimal Weight_kg { get; set; }
        public int Warehouse_id { get; set; }

    }
}