using System.Xml.Schema;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using CoreLayer.DTOs.Product;
using CoreLayer.Services.OrderDetails;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using DataLayer.Entities.Orders;


namespace BatteryShop_Web.Pages.Utilities;


public class AddOrderDetail
{
    private readonly IOrderService _order;
    private readonly IOrderDetailService _orderDetail;

    public AddOrderDetail(IOrderService order, IOrderDetailService orderDetail)
    {
        _order = order;
        _orderDetail = orderDetail;
    }

    public OperationResult AddOrder(ProductDto dto, int userId , int count, int price)
    {
        var result = _order.CreateOrder(new CreateOrderDto()
        {
            TotalPrice = (price * count),
            OrderStatus = OrderStatus.Pending,
            IsFinally = false,
            UserId = userId
        });
        var order = _order.GetOrderByUserId(userId);
        if (result.Status == OperationResultStatus.Success)
        {
            result = _orderDetail.AddOrderDetail(new AddOrderDetailDto()
            {
                Price = price,
                Count = count,
                ProductId = dto.Id,
                OrderId = order.Id,
            });
        }
        return result;
    }

}