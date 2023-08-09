using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.Product;
using CoreLayer.Services.Orders;
using CoreLayer.Services.Products;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BatteryShop_Web.Pages
{
    public class CartModel : PageModel
    {

        #region Services

        private readonly IOrderService _orderService;

        public CartModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region Properties

        public List<OrderDto>? OrderDTOs { get; set; }

        #endregion


        public IActionResult OnGet()
        {
            

            var userId = User.GetUserId();
            if (userId == null)
            {
                OrderDTOs = null;
                return Page();
            }
            OrderDTOs = _orderService.GetOrderBy(userId);
            return Page();
        }

        public IActionResult OnPost(long? id)
        {
            var userId = User.GetUserId();
            var result = _orderService.DeleteOrder(id,userId);

            OrderDTOs = _orderService.GetOrderBy(userId);
            return Page();

            
        }
        
    }
}
