using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.Product;

namespace CoreLayer.DTOs.OrderDetails;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public OrderDto Order { get; set; }
    public ProductDto Product { get; set; }
}

public class AddOrderDetailDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
}