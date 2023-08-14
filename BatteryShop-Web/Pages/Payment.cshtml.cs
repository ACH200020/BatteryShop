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

        private List<OrderDto> Orders { get; set; }

        public IActionResult OnGet()
        {
            long totalPrice =0;
            Orders = _orderService.GetOrderByUserId(User.GetUserId());
            foreach (var order in Orders)
            {
                if(order.IsFinally == false)
                    for (int i = 0; i < order.Count; i++)
                        totalPrice += order.Price;

            }

            var payment = new Payment((int)totalPrice);
            var result = payment.PaymentRequest("پرداخت شماره فاکتور 1",
                "http://localhost:59046/FinalOrder",
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
