using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class SyncDetailsDto
    {
        [Required]
        public int Month { get; set; }
        
        [Required]
        public int Year { get; set; }

        public long DataAmount { get; set; }
    }
}