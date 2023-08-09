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
        public static Order OrderCreateMapper(CreateOrderDto dto)
        {
            return new Order()
            {
                UserId = dto.UserId,
                Count = dto.Count,
                OrderStatus = dto.OrderStatus,
                Price = dto.Price,
                ProductId = dto.ProductId,

            };
        }
    }
}
