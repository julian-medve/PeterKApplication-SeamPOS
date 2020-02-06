using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class AuthStaffReqDto
    {
        [Required]
        public string Pin { get; set; }
    }
    
    public class AuthStaffResDto
    {
        public string StaffToken { get; set; }
    }
}