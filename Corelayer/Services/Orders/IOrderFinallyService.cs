using CoreLayer.DTOs.Order;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Orders;

public interface IOrderFinallyService
{
    OperationResult AddOrderFinally(AddOrderFinallyDto  orderFinallyDto);
    List<OrderFinallyDto> GetListByUserId(long UserId);
}

public class OrderFinallyService : IOrderFinallyService
{

    private readonly ShopContext _shopContext;

    public OrderFinallyService(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public OperationResult AddOrderFinally(AddOrderFinallyDto orderFinallyDto)
    {
        var finallyOrder = new OrderFinally()
        {
            OrderFinallyTime = DateTime.Now,
            IsFinal = false,
            ProductId = orderFinallyDto.ProductId,
            UserId = orderFinallyDto.UserId,
            UserAddressId = orderFinallyDto.UserAddressId,
        };
        _shopContext.OrderFinallies.Add(finallyOrder);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public List<OrderFinallyDto> GetListByUserId(long UserId)
    {
        return _shopContext.OrderFinallies
            .Include(f => f.Product)
            .Include(f => f.User)
            .Include(f => f.UserAddress)
            .Where(f => f.UserId == UserId)
            .Select(f => new OrderFinallyDto()
            {
                UserAddressId = f.UserAddressId,
                IsFinal = f.IsFinal,
                ProductId = f.ProductId,
                UserId = f.UserId,
                OrderFinallyTime = f.OrderFinallyTime,
            }).ToList();
    }
}