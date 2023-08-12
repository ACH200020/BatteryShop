using System.ComponentModel.DataAnnotations;

namespace BatteryShop_Web.Areas.UserPanel.Models.User
{
    public class EditUserViewModel
    {
        [Display(Name = " اسم")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Name { get; set; }

        [Display(Name = " فامیلی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Family { get; set; }

        [Display(Name = " کد ملی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string NationalCode { get; set; }
    }
}
