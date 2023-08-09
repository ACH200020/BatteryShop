using BatteryShop_Web.Areas.Admin.Models.Categories;
using CoreLayer.DTOs.Category;
using CoreLayer.Services.Categories;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategory());
        }


        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createCategory)
        {
            createCategory.ParentId = parentId;
            var result = _categoryService.CreateCategory(createCategory.MapToDto());

            return RedirectAndShowAlert(result, RedirectToAction("Index"));

        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                Title = category.Title,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editCategory)
        {
            var result = _categoryService.EditCategory(new EditCategoryDto()
            {
                Id = id,
                MetaDescription = editCategory.MetaDescription,
                MetaTag = editCategory.MetaTag,
                Slug = editCategory.Slug,
                Title = editCategory.Title,
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editCategory.Slug), result.Message);
                return View();
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var model = _categoryService.GetChildCategories(parentId);
            return new JsonResult(model);
        }
    }
}
