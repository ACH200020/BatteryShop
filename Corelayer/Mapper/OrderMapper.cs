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
                Count = dto.Count,
                OrderStatus = dto.OrderStatus,
                Price = dto.Price,
                ProductId = dto.ProductId,
                IsFinally = dto.IsFinally,
            };
        }

        public static Order EditOrderMapper(EditOrderDto dto, Order order)
        {
            order.Address = dto.Address;
            order.OrderStatus = (OrderStatus)dto.OrderStatus;
            order.IsFinally = (bool)(dto.IsFinally == null?order.IsFinally:dto.IsFinally);
            order.PostalCode = dto.PostalCode;
            order.PaymentTime = dto.PaymentTime;
            return order;
        }

        public static OrderDto MapToDto(Order order)
        {
            return new OrderDto()
            {
                Price = order.Price,
                UserId = order.UserId,
                Count = order.Count,
                Id = order.Id,
                OrderStatus = order.OrderStatus,
                ProductId = order.ProductId,
                Product = order.Product,
                Address = order.Address,
                User = order.User,
                IsFinally = order.IsFinally,
                PostalCode = order.PostalCode,
                PaymentTime = order.PaymentTime,
            };
        }
    }
}
