using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KE03_INTDEV_SE_1_Base.Pages.Cart
{
    public class IndexModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        public decimal Total { get; set; }

        public void OnGet()
        {
            var cartJson = HttpContext.Session.GetString("Cart");

            CartItems = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();

            Total = CartItems.Sum(i => i.Price * i.Quantity);
        }
    }
}