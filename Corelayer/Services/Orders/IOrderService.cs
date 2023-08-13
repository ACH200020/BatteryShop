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
using DataLayer.Entities.Orders;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Orders
{
    public interface IOrderService
    {
        OperationResult CreateOrder(CreateOrderDto createOrderDto);
        OperationResult EditOrder(EditOrderDto editOrderDto, int id);
        OperationResult RemoveOrder(int orderId);

        List<OrderDto> GetOrderByUserId(int id);
        OperationResult DeleteOrder(int? id, int? userId);
        List<OrderDto> OrdersThatPaymentFinaled();
        OrderDto? GetOrder(int id);
        List<OrderDto> GetFinaledOrderByUserId(int userId);
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

            var order = OrderMapper.CreateOrderMapper(createOrderDto);
            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();
            return OperationResult.Success();

        }

        public OperationResult EditOrder(EditOrderDto editOrderDto, int id)
        {
            var order = _shopContext.Orders.FirstOrDefault(f => f.Id == id);
            if(order == null)
                return OperationResult.NotFound();

            var newOrder = OrderMapper.EditOrderMapper(editOrderDto, order);
            _shopContext.Orders.Update(newOrder);
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

        public List<OrderDto> GetOrderByUserId(int id)
        {
            return  _shopContext.Orders
                .Include(f=>f.Product)
                .Where(f=>f.UserId == id)
                
                .Select(order=>OrderMapper.MapToDto(order))
                .AsQueryable().ToList();
        /*.Select(order=> new OrderDto()
            {
            Price = order.Price,
            UserId = order.UserId,
            Count = order.Count,
            Id = order.Id,
            OrderStatus = order.OrderStatus,
            ProductId = order.ProductId,
            Product = order.Product,
        })*/
    }

    public OperationResult DeleteOrder(int? id, int? userId)
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

        public List<OrderDto> OrdersThatPaymentFinaled()
        {
            return _shopContext.Orders.Where(o=>o.IsFinally == true && o.OrderStatus== OrderStatus.Sending)
                .Include(u => u.User)
                .Include(p=>p.Product)
                .Select(order=>OrderMapper.MapToDto(order))
                .ToList();
        }

        public OrderDto? GetOrder(int id)
        {
            var order = _shopContext.Orders.FirstOrDefault(f => f.Id == id);
            if (order == null)
            {
                return null;
            }
            return OrderMapper.MapToDto(order);
        }

        public List<OrderDto> GetFinaledOrderByUserId(int userId)
        {
            return _shopContext.Orders.Where(o=>o.OrderStatus == OrderStatus.Sended)
                .Include(u => u.User)
                .Include(p=>p.Product)
                .Select(order=>OrderMapper.MapToDto(order))
                .AsQueryable()
                .ToList();
        }
    }
}
