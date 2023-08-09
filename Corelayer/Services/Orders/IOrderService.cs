using CoreLayer.DTOs.Order;
using CoreLayer.Mapper;
using CoreLayer.Utilities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Orders
{
    public interface IOrderService
    {
        OperationResult CreateOrder(CreateOrderDto createOrderDto);
        OperationResult RemoveOrder(int orderId);

        List<OrderDto> GetOrderBy(long id);
        OperationResult DeleteOrder(long? id, long? userId);

    }

    public class OrderService : IOrderService
    {
        private readonly ShopContext _shopContext;
        public OrderService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public OperationResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var productId = _shopContext.Products.FirstOrDefault(f=>f.Id == createOrderDto.ProductId);
            if (productId == null)
                return OperationResult.NotFound();

            var userId = _shopContext.Users.FirstOrDefault(f => f.Id == createOrderDto.UserId);
            if (userId == null)
                return OperationResult.NotFound();
            
            if(productId.Count < createOrderDto.Count)
                return OperationResult.Error("تعداد وارد شده در انبار موجود نیست");

            var order = OrderMapper.OrderCreateMapper(createOrderDto);
            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();
            return OperationResult.Success();

        }

        public OperationResult RemoveOrder(int orderId)
        {
            var order= _shopContext.Orders.FirstOrDefault(f=>f.Id == orderId);
            if(order == null)
                return OperationResult.NotFound();
            
            _shopContext.Orders.Remove(order);
            _shopContext.SaveChanges(); 
            return OperationResult.Success();
        }

        public List<OrderDto> GetOrderBy(long id)
        {
            return  _shopContext.Orders
                .Include(f=>f.Product)
                .Where(f=>f.UserId == id)
                .Select(order=> new OrderDto()
                {
                Price = order.Price,
                UserId = order.UserId,
                Count = order.Count,
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                ProductId = order.ProductId,
                Product = order.Product,
                }).AsQueryable().ToList();
            
        }

        public OperationResult DeleteOrder(long? id,long? userId )
        {
            if (id != null)
            {
                var order = _shopContext.Orders.FirstOrDefault(f => f.Id == id);
                _shopContext.Remove(order);
                _shopContext.SaveChanges();
                return OperationResult.Success();
            }
            else
            {
                var order = _shopContext.Orders.Where(f => f.UserId == userId).ToList();
                _shopContext.Remove(order);
                _shopContext.SaveChanges();
                return OperationResult.Success();
            }
        }



    }
}
