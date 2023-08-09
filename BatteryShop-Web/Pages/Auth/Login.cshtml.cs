using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : RazorSweetAlertBase
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [Required(ErrorMessage = "شماره موبایل را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var user = _userService.LoginUser(new LoginUserDto()
            {
                Password = Password,
                PhoneNumber = PhoneNumber
            });

            if (user == null)
            {
                ModelState.AddModelError("PhoneNumber", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            if (!PhoneNumber.CheckIsNumber())
            {
                ModelState.AddModelError("PhoneNumber", "شماره تلفن را به درستی وارد کنید");
                return Page();
            }
            string fullName = user.Name + " " + user.Family;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,fullName),
                new Claim(ClaimTypes.Role,user.UserRole.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectAndShowAlert(OperationResult.Success(), RedirectToPage("../Index"));
        }
    }
}
