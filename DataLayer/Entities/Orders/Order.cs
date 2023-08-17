using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.OrderDetails;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities.Orders;

public class Order : BaseEntities
{

    public int UserId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int TotalPrice { get; set; }
    public bool IsFinally { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public DateTime? PaymentTime { get; set; }
    public string? TrackingCode { get; set; }


    #region Relations


    [ForeignKey("UserId")]
    public User User { get; set; }


    public ICollection<OrderDetail> OrderDetails { get; set; }
    #endregion
}

public enum OrderStatus
{
    Pending,
    Buy,
    Sending,
    Sended
}