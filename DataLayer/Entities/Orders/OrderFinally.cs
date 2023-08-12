using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;

namespace DataLayer.Entities.Orders;

public class OrderFinally : BaseEntities
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public long UserAddressId { get; set; }
    public DateTime OrderFinallyTime { get; set; }
    public bool IsFinal { get; set; }


    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [ForeignKey("UserAddressId")]
    public UserAddress UserAddress { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}