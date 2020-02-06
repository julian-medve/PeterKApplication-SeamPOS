using System;
using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class PaymentPlanDto
    {
        public Guid? Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal CloudSyncMBs { get; set; }
        
        public int AdditionalStaff { get; set; }
        
        public int Locations { get; set; }
        
        public decimal PaymentOption { get; set; }
    }
}