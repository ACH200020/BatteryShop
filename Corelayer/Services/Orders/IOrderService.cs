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
using CoreLayer.DTOs.OrderDetails;
using CoreLayer.Services.OrderDetails;
using DataLayer.Entities.Orders;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Orders
{
    public interface IOrderService
    {
        OperationResult CreateOrder(CreateOrderDto createOrderDto);
        OperationResult EditOrder(EditOrderDto editOrderDto);
        void EditPrice(int price,int id,int count);
        OrderDto GetOrderByUserId(int id);
        OrderDto OrdersThatPaymentFinaled();
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
            
            var order =
                _shopContext.Orders.FirstOrDefault(o => o.UserId == createOrderDto.UserId && o.IsFinally == false);
            if (order == null)
            {
                var orders = OrderMapper.CreateOrderMapper(createOrderDto);
                _shopContext.Orders.Add(orders);

                _shopContext.SaveChanges();

                return OperationResult.Success();
            }
            return EditOrder(new EditOrderDto()
            {
                Id = order.Id,
                TotalPrice = createOrderDto.TotalPrice,
                OrderStatus = OrderStatus.Pending,
            });
            
            
            

        }
        
        public OperationResult EditOrder(EditOrderDto editOrderDto)
        {
            
            var order = _shopContext.Orders.FirstOrDefault(f => f.Id == editOrderDto.Id);
            if(order == null)
                return OperationResult.NotFound();

            var newOrder = OrderMapper.EditOrderMapper(editOrderDto, order);
            _shopContext.Orders.Update(newOrder);
            _shopContext.SaveChanges();
            return OperationResult.Success();
        }

        public void EditPrice(int price , int id, int count)
        {
            price *= count;
            var order = _shopContext.Orders.FirstOrDefault(f => f.Id == id);
            var orderDetailExists = _shopContext.OrderDetails.Any(f => f.OrderId == order.Id);
            order.TotalPrice -= price;
            if (order.TotalPrice == 0 || orderDetailExists == false)
            {
                _shopContext.Remove(order);
                _shopContext.SaveChanges();
            }
            else
            {
                _shopContext.Orders.Update(order);
                _shopContext.SaveChanges();
            }
            
        }

        public OrderDto GetOrderByUserId(int id)
        {
            var order = _shopContext.Orders.FirstOrDefault(f => f.UserId == id && f.IsFinally == false);
            if (order == null)
                return new OrderDto();
            var orderDto = OrderMapper.MapToDto(order);
            return orderDto;

        }

        public OrderDto OrdersThatPaymentFinaled()
        {
            var order = _shopContext.Orders.FirstOrDefault(f => f.IsFinally == true && f.OrderStatus == OrderStatus.Buy);
            return OrderMapper.MapToDto(order);
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
                .Select(order=>OrderMapper.MapToDto(order))
                .AsQueryable()
                .ToList();
        }
    }
}
