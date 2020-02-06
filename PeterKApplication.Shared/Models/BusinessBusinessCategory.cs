using System;

namespace PeterKApplication.Shared.Models
{
    public class BusinessBusinessCategory: BaseEntity
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid BusinessCategoryId { get; set; }
        public BusinessCategory BusinessCategory { get; set; }
    }
}