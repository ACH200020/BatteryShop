using BatteryShop_Web.Areas.UserPanel.Models.Order;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.UserPanel.Controllers
{
    public class OrderFinallyController : UserPanelControllerBase
    {
        private readonly IOrderFinallyService _orderFinallyService;

        public OrderFinallyController(IOrderFinallyService orderFinallyService)
        {
            _orderFinallyService = orderFinallyService;
        }

        public IActionResult Index()
        {
            OrderViewModel order = new();
            order.OrderFinallyDtos = _orderFinallyService.GetListByUserId(User.GetUserId());
            
            return View(order);
        }
    }
}
