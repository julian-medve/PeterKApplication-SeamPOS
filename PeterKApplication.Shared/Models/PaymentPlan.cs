using System;

namespace PeterKApplication.Shared.Models
{
    public class PaymentPlan:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CloudSyncMBs { get; set; }
        public int AdditionalStaff { get; set; }
        public int Locations { get; set; }
        public decimal PaymentOption { get; set; }
    }
}