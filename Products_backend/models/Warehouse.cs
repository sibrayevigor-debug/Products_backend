using System.ComponentModel.DataAnnotations;

namespace Products_backend.models
{
    public class Warehouse
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
