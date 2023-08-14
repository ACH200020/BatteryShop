using System.ComponentModel.DataAnnotations;
using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.User;
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

        public CheckOutModel(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        #endregion

        #region Models

        public UserDto CurentUser { get; set; }
        public List<OrderDto> Orders { get; set; }
        public long AllSales { get; set; }
        public long AllPrice { get; set; }

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
            Orders = _orderService.GetOrderByUserId(User.GetUserId());
            int? sales = 0;
            foreach (var item in Orders)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (item.Product.Sales > 0)
                        sales = (item.Product.Sales);
                    AllSales += (long)(item.Product.Price * sales) / 100;
                    AllPrice += (long)(item.Price);
                }

            }

        }

        public IActionResult OnPost()
        {
            Orders = _orderService.GetOrderByUserId(User.GetUserId());

            foreach (var item in Orders)
            {
                EditOrderDto orderDto = new();
                orderDto.Address = FullAddress;
                orderDto.PostalCode = PostalCode;
                orderDto.OrderStatus = OrderStatus.Buy;
              
                _orderService.EditOrder(orderDto, item.Id);
            }



            return RedirectToPage("Index");


        }
    }
}
