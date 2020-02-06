using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class Order : BaseEntity
    {
        public long OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime? ShippedOn { get; set; }
        public string DeliveryAddress { get; set; }
        public string TransactionNumber { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Guid? BusinessId { get; set; }
        public Business Business { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public decimal DeliveryPrice { get; set; }
        public decimal Discount { get; set; }
        [NotMapped] public string DiscountLabel { get; set; }
        [NotMapped] public string DeliveryPriceLabel { get; set; }

        [NotMapped] public decimal Amount => OrderItems?.Sum(i => i.Price * i.Quantity) + DeliveryPrice ?? 0;
        [NotMapped] public string AmountLabel { get; set; }
        [NotMapped] public string StatusText => OrderStatus.ToString();
        [NotMapped] public List<Product> OrderProductItems { get; set; }

        [NotMapped] public int OrderProductsHeight { get; set; }
        public string Image
        {
            get
            {
                switch (OrderStatus)
                {
                    case OrderStatus.Cancelled: return "Cross_Red.png";
                    case OrderStatus.Paid: return "Tick_Green.png";
                    case OrderStatus.Pending: return "Pending_Yellow.png";
                    default: return null;
                }
            }
        }
    }
}