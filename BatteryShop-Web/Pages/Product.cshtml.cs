using BatteryShop_Web.Areas.Admin.Models.Products;
using BatteryShop_Web.Pages.Utilities;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CoreLayer.DTOs.Comment;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.Product;
using CoreLayer.DTOs.Product.ProductImage;
using CoreLayer.Mapper;
using CoreLayer.Services.Orders;
using CoreLayer.Services.Products;
using CoreLayer.Services.Products.ProductImages;
using CoreLayer.Utilities;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BatteryShop_Web.Pages
{
    public class ProductModel : RazorSweetAlertBase
    {
        #region Services

        private readonly IProductService _product;
        private readonly IProductImageService _productImageService;
        private readonly IOrderService _orderService;
        private readonly ICommentService _commentService;


        public ProductModel(IProductService product, IProductImageService productImageService,
            IOrderService orderService, ICommentService commentService)
        {
            _product = product;
            _productImageService = productImageService;
            _orderService = orderService;
            _commentService = commentService;
        }

        #endregion

        #region Models

        public ProductDto? ProductDto { get; set; }
        public List<ProductDto>? RelatedProduct { get; set; }
        public List<ImageDto> ProductImageDto { get; set; }
        public List<CommentDto> Comments { get; set; }

        private static ProductDto _productDto;

        [BindProperty]
        public int Count { get; set; }

        #endregion

        public IActionResult OnGet(string slug)
        {
            ProductDto = _product.GetProductBySlug(slug);
            if (ProductDto == null)
                return NotFound();
            ProductImageDto = _productImageService.GetImageById(ProductDto.Id);

            RelatedProduct = _product.GetProductByAmpere(ProductDto);
            _productDto = ProductDto;
            Comments = _commentService.GetProductComments(ProductDto.Id);

            return Page();

        }

        public IActionResult OnPost()
        {
            long? price;
            if (_productDto.Sales > 0)
            {
                price = _productDto.Price - (_productDto.Price * _productDto.Sales / 100);
            }
            else
                price = _productDto.Price;
            
            var result = _orderService.CreateOrder(new CreateOrderDto()
            {
                Price = (long)price,
                Count = Count,
                ProductId = _productDto.Id,
                OrderStatus = OrderStatus.Buy,
                UserId = User.GetUserId()
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ProductDto = _productDto;
                ProductImageDto = _productImageService.GetImageById(ProductDto.Id);
                RelatedProduct = _product.GetProductByAmpere(ProductDto);
                ModelState.AddModelError(nameof(Count), result.Message);
                return Page();
            }
            return RedirectAndShowAlert(result, RedirectToPage("Cart"));

        }



    }
}
