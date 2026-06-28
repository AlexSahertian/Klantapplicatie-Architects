using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;

namespace KE03_INTDEV_SE_1_Base.Pages.Orders
{
    public class OrderStatusModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public Order? Order { get; set; }

        public OrderStatusModel(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = _orderRepository.GetOrderById((int) id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public string GetOrderStatus()
        {
            if (Order == null)
            {
                throw new Exception();
            }

            return Order.Status switch
            {
                OrderStatus.NONE => "Geen",
                OrderStatus.PROCESSING => "In Behandeling",
                OrderStatus.DELIVERING => "Bezorger Onderweg",
                OrderStatus.DELIVERED => "Afgeleverd",
                OrderStatus.DEPOT => "Op Afhaalpunt",
                _ => throw new Exception("unknown order status " + Order.Status),
            };
        }
    }
}