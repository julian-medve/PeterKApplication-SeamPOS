using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class AuthOwnerReqDto
    {
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
    
    public class AuthOwnerResDto
    {
        public string Token { get; set; }
    }
}