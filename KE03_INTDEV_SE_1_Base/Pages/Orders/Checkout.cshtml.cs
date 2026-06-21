using DataAccessLayer.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace KE03_INTDEV_SE_1_Base.Pages.Orders
{
    public class CheckoutModel : PageModel
    {
        // =========================
        // FORMULIER
        // =========================

        [BindProperty]
        [Required(ErrorMessage = "Naam is verplicht")]
        public string FullName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres is verplicht")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postcode is verplicht")]
        public string PostalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Woonplaats is verplicht")]
        public string City { get; set; }

        // =========================
        // WINKELWAGEN
        // =========================

        public List<CartItem> CartItems { get; set; } = new();

        public decimal Total { get; set; }

        // =========================
        // GET
        // =========================

        public void OnGet()
        {
            LoadCart();
        }

        // =========================
        // POST
        // =========================

        public IActionResult OnPost()
        {
            LoadCart();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Winkelwagen leegmaken
            HttpContext.Session.Remove("Cart");

            TempData["Success"] =
                "Bestelling succesvol geplaatst!";

            return RedirectToPage("/Orders/Success");
        }

        // =========================
        // HELPER
        // =========================

        private void LoadCart()
        {
            var cartJson =
                HttpContext.Session.GetString("Cart");

            CartItems = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson)
                    ?? new List<CartItem>();

            Total =
                CartItems.Sum(i => i.Price * i.Quantity);
        }
    }
}