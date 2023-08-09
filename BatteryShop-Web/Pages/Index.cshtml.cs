using CoreLayer.DTOs;
using CoreLayer.DTOs.Product;
using CoreLayer.Services;
using CoreLayer.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages
{
    public class IndexModel : PageModel
    {

        #region Services

        private readonly IProductService _productService;

        public IndexModel( IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Models

        public List<ProductDto> ProductDTOs { get; set; }

        #endregion

        public void OnGet()
        {
            ProductDTOs = _productService.GetAllProduct();
        }

        public IActionResult OnGetShortShow(string categorySlug)
        {
            return Partial("_ShortShow",ProductDTOs);
        }
    }
}