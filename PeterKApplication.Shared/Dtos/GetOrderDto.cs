using System;
using System.ComponentModel.DataAnnotations;

namespace PeterKApplication.Shared.Dtos
{
    public class GetOrderReqDto
    {
        [Required]
        public Guid? Id { get; set; }
    }

    public class GetOrderResDto
    {
        
    }
}