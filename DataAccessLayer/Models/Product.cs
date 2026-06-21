namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<Order> Orders { get; set; } = new();
        public List<Part> Parts { get; set; } // Add this property to fix CS1061
    }
}