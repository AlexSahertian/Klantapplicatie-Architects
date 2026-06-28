using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        public bool Active { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}