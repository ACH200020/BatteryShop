using BatteryShop_Web.Pages.Utilities;
using CodeYad_Blog.CoreLayer.Services.Comments;
using CoreLayer.DTOs.Comment;
using CoreLayer.Services.Products;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace BatteryShop_Web.Pages
{
    public class CommentModel : RazorSweetAlertBase
    {
        #region Services

        private readonly ICommentService _commentService;
        private readonly IProductService _productService;
        public CommentModel(ICommentService commentService, IProductService productService)
        {
            _commentService = commentService;
            _productService = productService;
        }

        #endregion

        #region Models

        [BindProperty]
        public string Text { get; set; }

        public List<CommentDto>? Comments { get; set; }

        #endregion

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("product", new { slug });
            var product = _productService.GetProductBySlug(slug);

            

            var result = _commentService.CreateComment(new CreateCommentDto()
            {
                ProductId = product.Id,
                Text = Text,
                UserId = User.GetUserId(),
                
            });

            return RedirectAndShowAlert(result, RedirectToPage("Product", new { slug }));
        }
    }
}
