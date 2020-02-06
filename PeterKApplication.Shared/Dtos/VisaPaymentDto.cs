using System;
using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class VisaPaymentReqDto
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
    
    public class VisaPaymentResDto
    {
        public bool IsPaymentSuccessful { get; set; }
        public long TransactionIdentifier { get; set; }

        public string ActionCode { get; set; }

        public string ApprovalCode { get; set; }

        public string ResponseCode { get; set; }
    }
}