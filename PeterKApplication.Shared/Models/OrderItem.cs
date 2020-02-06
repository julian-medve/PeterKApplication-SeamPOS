using System;
using System.ComponentModel.DataAnnotations;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}