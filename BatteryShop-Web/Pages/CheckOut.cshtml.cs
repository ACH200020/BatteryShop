using System.ComponentModel.DataAnnotations;
using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Orders;
using CoreLayer.Services.Users;
using CoreLayer.Services.Users.UserAddress;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace BatteryShop_Web.Pages
{
    [ValidateAntiForgeryToken]

    public class CheckOutModel : RazorSweetAlertBase
    {
        #region Services

        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;

        public CheckOutModel(IUserService userService, IOrderService orderService, IAddressService addressService)
        {
            _userService = userService;
            _orderService = orderService;
            _addressService = addressService;
        }

        #endregion

        #region Models

        public UserDto CurentUser { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<UserAddressDto> UserAddresses { get; set; }


        /*OnPost*/

        [BindProperty]
        [Display(Name = " کدپستی")]
        public string? PostalCode { get; set; }
        [BindProperty]
        [Display(Name = " آدرس")]
        [DataType(DataType.MultilineText)]
        public string? FullAddress { get; set; }

        [BindProperty]
        [Display(Name = " انتخاب از بین آدرس های وارد شده")]
        public long? AddressId { get; set; }

        public long AllSales { get; set; }
        public long AllPrice { get; set; }
        #endregion

        public void OnGet()
        {
            CurentUser = _userService.GetUserById(User.GetUserId());
            UserAddresses = _addressService.GetAddressByUserId(User.GetUserId());
            Orders = _orderService.GetOrderBy(User.GetUserId());
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
            if (PostalCode == null && FullAddress == null && AddressId == null)
            {
                ModelState.AddModelError(nameof(FullAddress), "لطفا آدرسی را انتخاب کنید یا آدرس جدید وارد بکنید");
                CurentUser = _userService.GetUserById(User.GetUserId());
                UserAddresses = _addressService.GetAddressByUserId(User.GetUserId());
                Orders = _orderService.GetOrderBy(User.GetUserId());
                return Page();
            }

            if (!PostalCode.CheckIsNumber() && PostalCode != null)
            {
                ModelState.AddModelError(nameof(PostalCode),"لطفا فقط عدد وارد کنید");
                CurentUser = _userService.GetUserById(User.GetUserId());
                UserAddresses = _addressService.GetAddressByUserId(User.GetUserId());
                Orders = _orderService.GetOrderBy(User.GetUserId());
                return Page();
            }

            if (AddressId != null)
            {
                var address = _addressService.GetAddressById(AddressId);
                FullAddress = address.Address;
                PostalCode = address.PostalCode;
                return RedirectToPage("Index");
            }

            var result = _addressService.AddUserAddress(new AddUserAddressDto()
            {
                ActiveAddress = true,
                Address = FullAddress,
                City = "قم",
                Shire = "قم",
                PostalCode = PostalCode,
                UserId = User.GetUserId(),
            });


            return RedirectAndShowAlert(result, RedirectToPage("Index"));


        }
    }
}
