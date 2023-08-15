using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.OrderDetails;
using CoreLayer.Mapper;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Products;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.OrderDetails;

public interface IOrderDetailService
{
    OperationResult AddOrderDetail(AddOrderDetailDto  orderDetailDto);

    List<OrderDetailsDto> GetOrderDetailByOrderId(int orderId);
    OperationResult RemoveOrderDetail(int id);
    OrderDetailsDto GetOrderDetail(int id);
}

public class OrderDetailService : IOrderDetailService
{

    private readonly ShopContext _shopContext;

    public OrderDetailService(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public OperationResult AddOrderDetail(AddOrderDetailDto orderDetailDto)
    {
        var product = _shopContext.Products.FirstOrDefault(f => f.Id == orderDetailDto.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        if (product.Count < orderDetailDto.Count)
            return OperationResult.Error("تعداد وارد شده در انبار موجود نیست");


        var orderDetail = OrderDetailMapper.CreateOrderDetailMapper(orderDetailDto);
        _shopContext.OrderDetails.Add(orderDetail);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public List<OrderDetailsDto> GetOrderDetailByOrderId(int orderId)
    {

        var orderDetail = _shopContext.OrderDetails.Where(o => o.OrderId == orderId)
            .Include(o => o.Product)
            .Include(o => o.Order)
            .Select(orderDetail => OrderDetailMapper.MapToDto(orderDetail))
            .ToList();

        if (orderDetail == null)
            return new List<OrderDetailsDto>();
        return orderDetail;
    }

    public OperationResult RemoveOrderDetail(int id)
    {
        var orderDetail = _shopContext.OrderDetails.FirstOrDefault(f => f.Id == id);
        if(orderDetail == null)
            return OperationResult.NotFound();

        _shopContext.OrderDetails.Remove(orderDetail);
        /*_orderService.EditPrice(orderDetail.Price,orderDetail.OrderId);*/
        return OperationResult.Success();
    }

    public OrderDetailsDto GetOrderDetail(int id)
    {
        var order =  _shopContext.OrderDetails.FirstOrDefault(o => o.Id == id);
        return OrderDetailMapper.MapToDto(order);
    }
}