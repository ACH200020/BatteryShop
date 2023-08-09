using DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.Order
{
    public class OrderDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long Price { get; set; }
        public DataLayer.Entities.Products.Product Product { get; set; }
    }

    public class CreateOrderDto
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long Price { get; set; }
    }


}
