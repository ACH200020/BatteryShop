using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities.Orders;

public class Order : BaseEntities
{
    public long UserId { get; set; }
    
    public long ProductId { get; set; }
    public int Count { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public long Price { get; set; }


    #region Relations

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    #endregion
}

public enum OrderStatus
{
    Pending,
    Cost,
    Buy,
    Sending
}