using BatteryShop_Web.Areas.Admin.Models.Products;
using BatteryShop_Web.Areas.UserPanel.Models.User;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.UserPanel.Controllers
{
    public class UserController : UserPanelControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService= userService;
        }

        public IActionResult Edit()
        {
            var user = _userService.GetUserById(User.GetUserId());
            var model = new EditUserViewModel()
            {
                NationalCode = user.NationalCode,
                Name = user.Name,
                Family = user.Family,   
            };

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.GetUserById(User.GetUserId());
            var result = _userService.EditUser(new EditUserDto()
            {
                NationalCode = model.NationalCode,
                Name = model.Name,
                Family = model.Family,
                Id = user.Id
            });

            return RedirectAndShowAlert(result, Redirect("/userpanel"));


        }



        public  IActionResult ChangePassword()
        { 
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.GetUserById(User.GetUserId());
            var result = _userService.ChangePassword(new ChangePasswordDto()
            {
                Id = user.Id,
                NewPassword = model.NewPassword,
                Password = model.Password,
                RepeatNewPassword = model.RepeatNewPassword,
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(EditPasswordViewModel.RepeatNewPassword), result.Message);
                return View(model);
            }

            return RedirectAndShowAlert(result, Redirect("/userpanel"));

        }
    }
}
