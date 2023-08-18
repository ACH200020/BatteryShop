using CoreLayer.DTOs.Order;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZarinpalSandbox;

namespace BatteryShop_Web.Pages
{
    [Authorize]
    public class PaymentModel : PageModel
    {

        private readonly IOrderService _orderService;

        public PaymentModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private OrderDto Order { get; set; }

        public IActionResult OnGet()
        {
            
            Order = _orderService.GetOrderByUserId(User.GetUserId());
            

            var payment = new Payment(Order.TotalPrice);
            var result = payment.PaymentRequest("پرداخت شماره فاکتور 1",
                "http://localhost:59046/",
                User.GetUserPhoneNumber());

            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
