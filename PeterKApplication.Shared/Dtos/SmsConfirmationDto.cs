using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class SmsConfirmationReqDto
    {
        [Required, Phone]
        public string PhoneNumber { get; set; }
    }

    public class SmsConfirmationResDto
    {
        public bool IsConfirmationCodeSent { get; set; }
        public string ConfirmationCode { get; set; }
    }
}