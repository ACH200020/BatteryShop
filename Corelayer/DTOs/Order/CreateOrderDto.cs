using DataLayer.Entities.Orders;

namespace CoreLayer.DTOs.Order;

public class CreateOrderDto
{
    public int UserId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int TotalPrice { get; set; }
    public bool IsFinally { get; set; }

}