using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.DTOs.Comment;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities.Comments;
using Microsoft.EntityFrameworkCore;

namespace CodeYad_Blog.CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetProductComments(long ProductId);
    }
    public class CommentService : ICommentService
    {
        private readonly ShopContext _context;

        public CommentService(ShopContext context)
        {
            _context = context;
        }

        public OperationResult CreateComment(CreateCommentDto command)
        {
            var comment = new Comment()
            {
                ProductId = command.ProductId,
                Text = command.Text,
                UserId = command.UserId,
                DateTime = DateTime.Now
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CommentDto> GetProductComments(long ProductId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.ProductId == ProductId)
                .Select(comment => new CommentDto()
                {
                    ProductId = comment.ProductId,
                    Text = comment.Text,
                    UserFullName = comment.User.Name + " " + comment.User.Family,
                    Id = comment.Id,
                    DateTime = comment.DateTime
                }).ToList();
        }
    }
}