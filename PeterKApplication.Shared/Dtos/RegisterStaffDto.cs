using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class RegisterStaffReqDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }
    }
}