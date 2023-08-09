using BatteryShop_Web.Areas.Admin.Models.Products;
using BatteryShop_Web.Areas.UserPanel.Models.Address;
using CoreLayer.DTOs.Product;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Users.UserAddress;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.UserPanel.Controllers
{
    public class AddressController : UserPanelControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            IndexAddress address = new();
            address.UserAddressDtos = _addressService.GetAddressByUserId(User.GetUserId());
            return View(address);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _addressService.AddUserAddress(new AddUserAddressDto()
            {
                ActiveAddress = model.ActiveAddress,
                City = model.City,
                PostalCode = model.PostalCode,
                Shire = model.Shire,
                UserId = User.GetUserId(),
                Address = model.Address,
            });
            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }


        public IActionResult Edit(int id)
        {
            var model = _addressService.GetAddressById(id);


            var address = new EditAddressViewModel()
            {
                ActiveAddress = model.ActiveAddress,
                City = model.City,
                PostalCode = model.PostalCode,
                Shire = model.Shire,
                Address = model.Address
            };
            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditAddressViewModel model , int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            var result = _addressService.EditUserAddress(new EditUserAddressDto()
            {
                Shire = model.Shire,
                ActiveAddress = model.ActiveAddress,
                City = model.City,
                PostalCode = model.PostalCode,
                Address = model.Address,
                Id = id
            });

            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }
    }
}
