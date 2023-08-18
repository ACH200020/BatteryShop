using System.ComponentModel.DataAnnotations;
using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using CoreLayer.DTOs.User;
using CoreLayer.Services.OrderDetails;
using CoreLayer.Services.Orders;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using DataLayer.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BatteryShop_Web.Pages
{
    [ValidateAntiForgeryToken]
    [Authorize]
    public class CheckOutModel : RazorSweetAlertBase
    {
        #region Services

        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        public CheckOutModel(IUserService userService, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _userService = userService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        #endregion

        #region Models

        public UserDto CurentUser { get; set; }
        public OrderDto Order { get; set; }
        public int AllSales { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }

        /*OnPost*/

        [BindProperty]
        [Display(Name = " کدپستی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string PostalCode { get; set; }

        [BindProperty]
        [Display(Name = " آدرس")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        [DataType(DataType.MultilineText)]
        public string FullAddress { get; set; }



        #endregion

        public void OnGet()
        {
            CurentUser = _userService.GetUserById(User.GetUserId());
            Order = _orderService.GetOrderByUserId(User.GetUserId());
            OrderDetails = _orderDetailService.GetOrderDetailByOrderId(Order.Id);

           

            int? sales = 0;
            AllSales += (int)(Order.TotalPrice * sales) / 100;
            
        }

        public IActionResult OnPost()
        {
            Order = _orderService.GetOrderByUserId(User.GetUserId());

            
            _orderService.EditOrder(new EditOrderDto()
            {
                Id = Order.Id,
                Address = FullAddress,
                PostalCode = PostalCode,
                OrderStatus = OrderStatus.Buy
            });

            return RedirectToPage("Index");


        }
    }
}
