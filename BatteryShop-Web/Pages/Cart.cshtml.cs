using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using CoreLayer.Services.OrderDetails;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BatteryShop_Web.Pages
{
    [Authorize]

    public class CartModel : PageModel
    {

        #region Services

        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        public CartModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        #endregion

        #region Properties

        public List<OrderDetailsDto>? OrderDetailDTOs { get; set; }
        public OrderDto? OrderDto { get; set; }
        #endregion


        public IActionResult OnGet()
        {
            

            var userId = User.GetUserId();
            if (userId == null)
            {
                OrderDetailDTOs = null;
                return Page();
            }

            OrderDto = _orderService.GetOrderByUserId(User.GetUserId());
            OrderDetailDTOs = _orderDetailService.GetOrderDetailByOrderId(OrderDto.Id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var userId = User.GetUserId();
            RemoveOrderDetail orderDetail = new RemoveOrderDetail(_orderService, _orderDetailService);
            orderDetail.EditOrder(id, userId);

            OrderDto = _orderService.GetOrderByUserId(userId);
            if(OrderDto != null)
                OrderDetailDTOs = _orderDetailService.GetOrderDetailByOrderId(OrderDto.Id);
            return Page();

            
        }
        
    }
}
