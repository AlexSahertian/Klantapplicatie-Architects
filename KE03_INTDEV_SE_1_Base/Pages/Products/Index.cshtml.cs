using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KE03_INTDEV_SE_1_Base.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<Product> Products { get; set; } = new();

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts().ToList();
        }

        public IActionResult OnPost(int productId)
        {
            Products = _productRepository.GetAllProducts().ToList();

            var product = Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return Page();
            }

            var cartJson = HttpContext.Session.GetString("Cart");

            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            TempData["SuccessMessage"] = $"{product.Name} is toegevoegd aan je winkelwagen.";

            return RedirectToPage("/Cart/Index");
        }
    }
}