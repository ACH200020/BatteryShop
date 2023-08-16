using CoreLayer.DTOs.Category;
using DataLayer.Entities.Categories;

namespace CoreLayer.Mapper
{
    public class CategoryMapper
    {
        public static CategoryDto MapToDto(Category category)
        {
            if(category == null)
                return new CategoryDto();

            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                ParentId = category.ParentId,
                Id = category.Id,
                Title = category.Title
            };
        }
    }
}