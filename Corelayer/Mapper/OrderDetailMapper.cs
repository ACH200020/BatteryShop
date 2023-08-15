using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using DataLayer.Entities.OrderDetails;
using DataLayer.Entities.Orders;

namespace CoreLayer.Mapper;

public class OrderDetailMapper
{
    public static OrderDetailsDto MapToDto(OrderDetail orderDetail)
    {
        return new OrderDetailsDto()
        {
            Price = orderDetail.Price,
            Count = orderDetail.Count,
            Id = orderDetail.Id,
            ProductId = orderDetail.ProductId,
            Order = OrderMapper.MapToDto(orderDetail.Order),
            OrderId = orderDetail.OrderId,
            Product = ProductMapper.MapToDto(orderDetail.Product),
        };

    }

    public static OrderDetail CreateOrderDetailMapper(AddOrderDetailDto orderDetail)
    {
        return new OrderDetail()
        {
            Price = orderDetail.Price,
            Count = orderDetail.Count,
            OrderId = orderDetail.OrderId,
            ProductId = orderDetail.ProductId,
        };
    }
}