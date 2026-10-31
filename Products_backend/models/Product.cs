using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Products_backend.models
{
    public enum Availability
    {
        OUT_OF_STOCK,
        LOW,
        MANY
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Length_mm { get; set; }
        public int Width_mm { get; set; }
        public int Height_mm { get; set; }
        public decimal Weight_kg { get; set; }
        public int Warehouse_id { get; set; }

        [ForeignKey(nameof(Warehouse_id))]
        public Warehouse Warehouse { get; set; } = null!;

        /// Атрибут [NotMapped] указывает EF Core, что это поле не нужно создавать в виде колонки в БД, 
        [NotMapped]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Чтобы в JSON уходило строковое значение (MANY), а не число (2)
        public Availability Availability
        {
            get
            {
                if (Quantity == 0) return Availability.OUT_OF_STOCK;
                if (Quantity >= 1 && Quantity <= 5) return Availability.LOW;
                return Availability.MANY;
            }
        }
    }
}
