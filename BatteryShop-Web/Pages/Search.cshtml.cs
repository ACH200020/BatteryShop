using CoreLayer.DTOs.Product;
using CoreLayer.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages
{

    public class SearchModel : PageModel
    {
        #region Services

        private readonly IProductService _productService;


        public SearchModel(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Models

        public ProductFilterDto Filter { get; set; }


        #endregion


        public void OnGet(int pageId = 1, string categorySlug = null , string title = null)
        {
            Filter = _productService.GetProductByFilterDto(new ProductFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 5,
                Title = title
            });
        }

        public IActionResult OnGetPagination(int pageId = 1, string categorySlug = null, string title = null)
        {
            var model = _productService.GetProductByFilterDto(new ProductFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 5,
                Title = title

            });
            return Partial("_SearchView", model);
        }
    }
}
