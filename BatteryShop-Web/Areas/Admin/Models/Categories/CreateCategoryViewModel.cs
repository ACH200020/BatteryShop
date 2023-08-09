using CoreLayer.DTOs.Category;
using System.ComponentModel.DataAnnotations;

namespace BatteryShop_Web.Areas.Admin.Models.Categories
{
    public class CreateCategoryViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Title { get; set; }

        [Display(Name = "slug")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string Slug { get; set; }


        public int? ParentId { get; set; }

        [Display(Name = "با - کلمات را از هم جدا کنید")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }


        public CreateCategoryDto MapToDto()
        {
            return new CreateCategoryDto()
            {
                Title = Title,
                Slug = Slug,
                ParentId = ParentId,
                MetaTag = MetaTag,
                MetaDescription = MetaDescription,
            };
        }
    }


}
