using CoreLayer.DTOs.Category;
using DataLayer.Entities.Categories;

namespace CoreLayer.Mapper
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                ParentId = (int?)category.ParentId,
                Id = (int)category.Id,
                Title = category.Title
            };
        }
    }
}