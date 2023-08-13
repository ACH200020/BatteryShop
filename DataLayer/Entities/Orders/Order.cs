using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities.Orders;

public class Order : BaseEntities
{

    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public long Price { get; set; }
    public bool IsFinally { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public DateTime? PaymentTime { get; set; }

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
    Buy,
    Sending,
    Sended
}