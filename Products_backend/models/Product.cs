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
        [Key]
        public int Id { get; set; }

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
        [Range(0.001, double.MaxValue)]
        public decimal Weight_kg { get; set; }

        [Required]
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
