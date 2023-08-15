using DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.DTOs.Product;
using CoreLayer.DTOs.User;

namespace CoreLayer.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalPrice { get; set; }

        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsFinally { get; set; }
        public UserDto User { get; set; }
        public DateTime? PaymentTime { get; set; }

    }
}
