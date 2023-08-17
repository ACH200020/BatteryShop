using DataLayer.Entities.Orders;

namespace CoreLayer.DTOs.Order;

public class EditOrderDto
{
    public int Id { get; set; }
    public int TotalPrice { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public bool? IsFinally { get; set; }
    public DateTime? PaymentTime { get; set; }
    public string? TrackingCode { get; set; }
}