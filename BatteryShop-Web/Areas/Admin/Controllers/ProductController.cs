using BatteryShop_Web.Areas.Admin.Models.Products;
using CoreLayer.DTOs.Product;
using CoreLayer.Services.Products;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers
{
    public class ProductController : AdminControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new ProductFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 1
            };

            var product = _productService.GetProductByFilterDto(param);
            return View(product);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            int sales = 0;

            if (!int.TryParse(viewModel.Price, out int price))
            {
                ModelState.AddModelError(nameof(CreateProductViewModel.Slug), "لطفا فقط عدد وارد بکنید");
                return View(viewModel);
            }

            if(viewModel.Sales != null)
                if (!int.TryParse(viewModel.Sales, out sales))
                {
                    ModelState.AddModelError(nameof(CreateProductViewModel.Slug), "لطفا فقط عدد وارد بکنید");
                    return View(viewModel);
                }


            var result = _productService.CreateProduct(new CreateProductDto()
            {
                Title = viewModel.Title,
                CategoryId = viewModel.CategoryId,
                Count = viewModel.Count,
                Description = viewModel.Description,
                ImageFile = viewModel.ImageFile,
                SeoData = viewModel.SeoData,
                Slug = viewModel.Slug,
                SubCategoryId = viewModel.SubCategoryId == 0 ? null : viewModel.SubCategoryId,
                Price = price,
                Sales = sales == 0?0:sales,
                Ampere = viewModel.Ampere,
                Brand = viewModel.Brand
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreateProductViewModel.Slug), result.Message);
                return View(viewModel);
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return RedirectToAction("Index");
            long price;

            var model = new EditProductViewModel()
            {
                Title = product.Title,
                CategoryId = product.CategoryId,
                Count = product.Count,
                Description = product.Description,
                SeoData = product.SeoData,
                Slug = product.Slug,
                SubCategoryId = product.SubCategoryId,
                Sales = product.Sales.ToString(),
                Price = product.Price.ToString(),
                Ampere = product.Ampere,
                Brand = product.Brand
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            

            if (!(int.TryParse(viewModel.Price, out int price) && int.TryParse(viewModel.Sales, out int sales)))
            {
                ModelState.AddModelError(nameof(EditProductViewModel.Slug), "لطفا فقط عدد وارد در قیمت و میزان تخفیف بکنید");
                return View(viewModel);
            }
            var result = _productService.EditProduct(new EditProductDto()
            {
                Title = viewModel.Title,
                CategoryId = viewModel.CategoryId,
                Count = viewModel.Count,
                Description = viewModel.Description,
                SeoData = viewModel.SeoData,
                Slug = viewModel.Slug,
                SubCategoryId = viewModel.SubCategoryId == 0 ? null : viewModel.SubCategoryId,
                ImageFile = viewModel.ImageFile,
                ProductId = id,
                Price = price,
                Sales = sales,
                Ampere = viewModel.Ampere,
                Brand = viewModel.Brand
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(EditProductViewModel.Slug), result.Message);
                return View(viewModel);
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));


        }

        
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
