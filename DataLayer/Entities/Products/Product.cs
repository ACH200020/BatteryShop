using DataLayer.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Categories;
using DataLayer.Entities.Comments;

namespace DataLayer.Entities.Products;

public class Product : BaseEntities
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public long Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }

    #region Relations

   

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    [ForeignKey("SubCategoryId")]
    public Category? SubCategory { get; set; }


    public ICollection<Comment> Comments { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; }

    public ICollection<Order> Order { get; set; }

    #endregion
}