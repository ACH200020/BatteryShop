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
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long Price { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsFinally { get; set; }
        public DataLayer.Entities.Users.User User { get; set; }
        public DataLayer.Entities.Products.Product Product { get; set; }
        public DateTime? PaymentTime { get; set; }

    }

    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long Price { get; set; }
        public bool IsFinally { get; set; }

    }

    public class EditOrderDto
    {
        public int Id { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsFinally { get; set; }
        public DateTime? PaymentTime { get; set; }
    }

}
