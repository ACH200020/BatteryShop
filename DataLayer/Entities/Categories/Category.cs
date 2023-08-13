using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Products;

namespace DataLayer.Entities.Categories;

public class Category : BaseEntities
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Slug { get; set; }
    public string MetaTag { get; set; }
    public string MetaDescription { get; set; }
    public int? ParentId { get; set; }


    [InverseProperty("Category")]
    public ICollection<Product> Products { get; set; }

    [InverseProperty("SubCategory")]
    public ICollection<Product> SubProducts { get; set; }
}