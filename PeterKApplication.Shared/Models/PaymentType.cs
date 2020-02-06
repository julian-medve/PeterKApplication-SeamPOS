using System;
using System.Collections.Generic;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<BusinessPaymentType> BusinessesPaymentTypes { get; set; }
    }
}