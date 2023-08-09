using CoreLayer.DTOs.User;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using BatteryShop_Web.Pages.Utilities;

namespace BatteryShop_Web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    [Authorize]
    public class ChangePasswordModel : RazorSweetAlertBase
    {
        private readonly IUserService _userService;


        #region Properties

        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string NewPassword { get; set; }

        [Display(Name = " تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string RepeatNewPassword { get; set; }


        #endregion
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            long userId = User.GetUserId();
            var result = _userService.ChangePassword(new ChangePasswordDto()
            {
                Id = userId,
                NewPassword = NewPassword,
                Password = "a8-b5-g7-h10-t5-Amir5565",
                RepeatNewPassword = RepeatNewPassword,
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError("RepeatNewPassword", "تکرار رمز عبور درست نمیباشد");
                return Page();
            }
            return RedirectAndShowAlert(result, RedirectToPage("Login"));

        }
    }
}
