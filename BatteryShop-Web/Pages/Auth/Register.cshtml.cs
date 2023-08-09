using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BatteryShop_Web.Pages.Utilities;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class RegisterModel : RazorSweetAlertBase
    {
        private readonly IUserService _userService;

        #region Properties

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} بایستی 11 رقم باشد")]
        [MinLength(11, ErrorMessage = "{0} بایستی 11 رقم باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MaxLength(10,ErrorMessage = "{0} 10 رقم باشد")]
        [MinLength(10,ErrorMessage = "{0} 10 رقم باشد")]
        public string NationalCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }

        #endregion

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var result = _userService.RegisterUser(new RegisterUserDto()
            {
                PhoneNumber = PhoneNumber,
                Password = Password,
                Name = Name,
                Family = Family,
                NationalCode = NationalCode

            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("PhoneNumber", result.Message);
                return Page();
            }

            
            if (!PhoneNumber.CheckIsNumber() || !NationalCode.CheckIsNumber())
            {
                ModelState.AddModelError("Name","کدملی یا شماره تلفن را به درستی وارد کنید");
                return Page();
            }
            return RedirectAndShowAlert(result, RedirectToPage("Login"));

        }
    }
}
