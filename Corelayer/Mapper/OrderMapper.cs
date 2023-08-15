using Azure.Core;
using CoreLayer.DTOs.Order;
using DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Mapper
{
    public class OrderMapper
    {
        public static Order CreateOrderMapper(CreateOrderDto dto)
        {
            return new Order()
            {
                UserId = dto.UserId,
                OrderStatus = dto.OrderStatus,
                TotalPrice = dto.TotalPrice,
                IsFinally = dto.IsFinally,
            };
        }

        public static Order EditOrderMapper(EditOrderDto dto, Order order)
        {
            order.Address = dto.Address;
            order.OrderStatus = (OrderStatus)dto.OrderStatus;
            order.IsFinally = (bool)(dto.IsFinally == null ? order.IsFinally:dto.IsFinally);
            order.PostalCode = dto.PostalCode;
            order.PaymentTime = dto.PaymentTime;
            order.TotalPrice += dto.TotalPrice;
            return order;
        }

        public static OrderDto MapToDto(Order order)
        {
            return new OrderDto()
            {
                TotalPrice = order.TotalPrice,
                UserId = order.UserId,
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                Address = order.Address,
                User = UserMapper.MapToUser(order.User),
                IsFinally = order.IsFinally,
                PostalCode = order.PostalCode,
                PaymentTime = order.PaymentTime,
            };
        }
    }
}
