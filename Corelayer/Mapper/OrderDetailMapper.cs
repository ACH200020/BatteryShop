using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using DataLayer.Entities.OrderDetails;
using DataLayer.Entities.Orders;

namespace CoreLayer.Mapper;

public class OrderDetailMapper
{
    public static OrderDetailsDto MapToDto(OrderDetail orderDetail)
    {
        if(orderDetail == null)
        {
            return new OrderDetailsDto();
        }
        return new OrderDetailsDto()
        {
            Price = orderDetail.Price,
            Count = orderDetail.Count,
            Id = orderDetail.Id,
            ProductId = orderDetail.ProductId,
            Order = orderDetail.Order == null ? new OrderDto() : OrderMapper.MapToDto(orderDetail.Order),
            OrderId = orderDetail.OrderId,
            Product = orderDetail.Product == null ? new DTOs.Product.ProductDto() : ProductMapper.MapToDto(orderDetail.Product),
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