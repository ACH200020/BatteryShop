using System.ComponentModel.DataAnnotations;
using CoreLayer.DTOs.User;

namespace BatteryShop_Web.Areas.UserPanel.Models.Address
{
    public class EditAddressViewModel
    {
        [Display(Name = " استان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Shire { get; set; }

        [Display(Name = " شهر")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string City { get; set; }

        [Display(Name = " کد پستی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string PostalCode { get; set; }

        [Display(Name = " فعال کردن آدرس")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public bool ActiveAddress { get; set; }

        [Display(Name = " آدرس")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Address { get; set; }
    }

    public class IndexAddress
    {
        public List<UserAddressDto>? UserAddressDtos { get; set; }
    }
}
