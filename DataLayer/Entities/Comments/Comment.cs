using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Comments
{
    public class Comment : BaseEntities
    {

        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
