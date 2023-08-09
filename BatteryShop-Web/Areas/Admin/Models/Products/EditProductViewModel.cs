using System.ComponentModel.DataAnnotations;

namespace BatteryShop_Web.Areas.Admin.Models.Products
{
    public class EditProductViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long CategoryId { get; set; }

        [Display(Name = "انتخاب زیر دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? SubCategoryId { get; set; }

        [Display(Name = "slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }

        [Display(Name = "Seo")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SeoData { get; set; }

        [Display(Name = "تعداد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Price { get; set; }

        [Display(Name = "میزان تخفیف")]
        public string? Sales { get; set; }

        [Display(Name = "آمپر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Ampere { get; set; }

        [Display(Name = "برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Brand { get; set; }
    }
}
