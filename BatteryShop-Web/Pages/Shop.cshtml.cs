using CoreLayer.DTOs.Product;
using CoreLayer.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages
{
    public class ShopModel : PageModel
    {
        #region Services

        private readonly IProductService _productService;

        public ShopModel(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Models

        public ProductFilterDto Products{ get; set; }


        #endregion

        public IActionResult OnGet(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new ProductFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 1
            };

             Products = _productService.GetProductByFilterDto(param);
             return Page();
        }
    }
}
