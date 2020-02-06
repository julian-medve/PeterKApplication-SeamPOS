using System;

namespace PeterKApplication.Shared.Models
{
    public class BusinessPaymentType: BaseEntity
    {
        public Guid? BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}