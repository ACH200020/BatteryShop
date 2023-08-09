using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.Comment
{
    public class CreateCommentDto
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public string Text { get; set; }
    }

    public class CommentDto
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public long ProductId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

    }
}
