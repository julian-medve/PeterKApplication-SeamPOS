using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class DeleteStaffReqDto
    {
        [Required]
        public string Id { get; set; }
    }
}