using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.Comment
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
    }

    public class CommentDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

    }
}
