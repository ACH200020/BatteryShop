using CoreLayer.DTOs.User;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index(string phoneNumber)
        {
            var param = new UserFilterParams() { PhoneNumber = phoneNumber };
            var model = _userService.GetUserByFilter(param);
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Edit(EditUserDto editUserDto)
        {
            var result = _userService.EditUser(editUserDto);
            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);

                return RedirectAndShowAlert(result, RedirectToAction("Index"));
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }


        public IActionResult ShowEditModal(int userId)
        {
            var user = _userService.GetUserById(userId);
            return PartialView("_EditUser", new EditUserDto()
            {
                Family = user.Family,
                Name = user.Name,
                NationalCode = user.NationalCode,
                UserRole = user.UserRole,
                Id = userId
            });
        }
        
    }
}
