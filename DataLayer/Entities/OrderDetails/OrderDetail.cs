using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Products;

namespace DataLayer.Entities.OrderDetails;

public class OrderDetailDto : BaseEntities
{
    public int ProductId { get; set; }

    public int OrderId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}