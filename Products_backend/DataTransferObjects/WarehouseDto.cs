namespace Products_backend.Data_Transfer_Objects
{
    // Для списка в каталоге (GET /api/warehouses)
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
