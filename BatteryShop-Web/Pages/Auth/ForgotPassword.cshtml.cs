using BatteryShop_Web.Areas.Admin.Models.Products;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace BatteryShop_Web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        #region Properties

        public ForgotPasswordModel(IUserService userService)
        {
            _userService = userService;
        }
        [Display(Name = " کد ملی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        [MaxLength(10, ErrorMessage = "{0} 10 رقم باشد")]
        [MinLength(10, ErrorMessage = "{0} 10 رقم باشد")]
        public string NationalCode { get; set; }


        #endregion
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var user = _userService.GetUserByNationalCode(NationalCode);
            if (user is null)
            {
                ModelState.AddModelError("NationalCode", "کدملی وارد شده یافت نشد");
                return Page();
            }

            if (!NationalCode.CheckIsNumber())
            {
                ModelState.AddModelError("NationalCode", "کدملی را به درستی وارد کنید");
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

            return RedirectToPage("ChangePassword");
        }
    }
}
