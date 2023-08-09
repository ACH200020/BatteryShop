using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products;

public class ProductImage : BaseEntities
{
    public long ProductId { get; set; }
    public string ImageName { get; set; }



    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}