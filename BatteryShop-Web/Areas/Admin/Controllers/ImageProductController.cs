using BatteryShop_Web.Areas.Admin.Models.ProductImage;
using CoreLayer.DTOs.Product.ProductImage;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Products.ProductImages;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers
{
    public class ImageProductController : AdminControllerBase
    {

        private readonly IProductImageService _productImageService;

        public ImageProductController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public IActionResult Index(string productTitle = "")
        {
            var param = new ImageFilterParams() { ProductTitle = productTitle };
            var model = _productImageService.GetAllImagesByFilterDto(param);
            
            return View(model);
        }


        
        

        public IActionResult Delete(long id)
        {
            _productImageService.DeleteImage(id);
            return RedirectToAction("Index");

        }


        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddProductImageViewModel productImageViewModel)
        {
            var productImages = new AddImageDto()
            {
                ImageFile = productImageViewModel.ImageFile,
                ProductId = productImageViewModel.ProductId,
            };

            _productImageService.AddImage(productImages);
            return RedirectToAction("Index");

        }
    }
}
