using System;
using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class MPesaPaymentReqDto
    {
        [Required]
        public Guid OrderId { get; set; }
    
        [Required]
        public string SenderName { get; set; }
    
        [Required]
        public string SenderAccountNumber { get; set; }
    
        [Required]
        public string Amount { get; set; }
    }
    
    public class MPesaPaymentResDto
    {
        public bool IsPaymentSuccessful { get; set; }
        public long TransactionIdentifier { get; set; }
    }
}