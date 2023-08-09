using CoreLayer.DTOs.Category;
using CoreLayer.Mapper;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto command);
        OperationResult EditCategory(EditCategoryDto command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetParentCategory();
        List<CategoryDto> GetChildCategories(int parentId);
        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
        bool IsSlugExist(string slug);
    }

    public class CategoryService : ICategoryService
    {

        private readonly ShopContext _shopContext;
        public CategoryService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }


        public OperationResult CreateCategory(CreateCategoryDto command)
        {
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("Slug Is Exist");

            var category = new Category()
            {
                Title = command.Title,
                ParentId = command.ParentId,
                Slug = command.Slug.ToSlug(),
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription
            };
            _shopContext.Categories.Add(category);
            _shopContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var category = _shopContext.Categories.FirstOrDefault(c => c.Id == command.Id);
            if (category == null)
                return OperationResult.NotFound();

            if (command.Slug.ToSlug() != category.Slug)
                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("Slug Is Exist");

            category.MetaDescription = command.MetaDescription;
            category.Slug = command.Slug.ToSlug();
            category.Title = command.Title;
            category.MetaTag = command.MetaTag;

            _shopContext.Categories.Update(category);
            _shopContext.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategory()
        {
            return _shopContext.Categories.Select(category => CategoryMapper.Map(category)).ToList();
        }

        public List<CategoryDto> GetParentCategory()
        {
            return _shopContext.Categories.Where(f=>f.ParentId == null)
                .Select(category => CategoryMapper.Map(category))
                .ToList();
            
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var category = _shopContext.Categories.FirstOrDefault(f => f.Id == id);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _shopContext.Categories.FirstOrDefault(c => c.Slug == slug);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _shopContext.Categories.Where(f=>f.ParentId == parentId)
                .Select(category => CategoryMapper.Map(category)).ToList() ;
        }

        public bool IsSlugExist(string slug)
        {
            return _shopContext.Categories.Any(c => c.Slug == slug.ToSlug());

        }
    }
}
