using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class UpdateOwnerPinDto
    {
        [Required, StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }
    }
}