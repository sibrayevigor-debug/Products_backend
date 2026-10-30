using System.ComponentModel.DataAnnotations;

namespace Products_backend.models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
