using DataLayer.Entities.Products;

namespace CoreLayer.DTOs.Order;


public class OrderFinallyDto
{

    public long Id { get; set; }

    public long UserId { get; set; }
    public long ProductId { get; set; }
    public long UserAddressId { get; set; }
    public DateTime OrderFinallyTime { get; set; }
    public bool IsFinal { get; set; }
}

public class AddOrderFinallyDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public long UserAddressId { get; set; }
}