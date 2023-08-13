using BatteryShop_Web.Areas.UserPanel.Models.Order;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.UserPanel.Controllers
{
    public class OrderController : UserPanelControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            ListOrderVIewModel model = new ListOrderVIewModel();
            model.OrderDtos = _orderService.GetFinaledOrderByUserId(User.GetUserId());
            return View(model);
        }
    }
}
