using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class UpdateOwnerReqDto
    {   
        [Required, MinLength(1)]
        public string FirstName { get; set; }

        [Required, MinLength(1)]
        public string LastName { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(3, MinimumLength = 2)]
        public string CountryCode { get; set; }
        
        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsAutoSyncEnabled { get; set; }
    }
}