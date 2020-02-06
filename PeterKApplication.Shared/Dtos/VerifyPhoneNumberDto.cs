using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class VerifyPhoneNumberReqDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string ConfirmationCode { get; set; }
    }

    public class VerifyPhoneNumberResDto
    {
        public bool IsPhoneNumberConfirmed { get; set; }
        public string Token { get; set; }
    }
}