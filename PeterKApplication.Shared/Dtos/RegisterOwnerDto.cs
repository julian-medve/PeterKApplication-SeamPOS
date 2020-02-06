using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class RegisterOwnerReqDto
    {
        [MinLength(1)]
        [Required]
        public string BusinessName { get; set; }

        [MinLength(1)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(1)]
        [Required]
        public string LastName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [StringLength(3, MinimumLength = 2)]
        [Required]
        public string CountryCode { get; set; }
        
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required] // TODO mislim da ti je ovo ispalo, provjeri na backendu jesu li isti ^
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    
    public class RegisterOwnerResDto
    {
        public bool IsConfirmationCodeSent { get; set; }
    }
}