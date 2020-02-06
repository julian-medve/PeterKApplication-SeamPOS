using System;

namespace PeterKApplication.Shared.Models
{
    public class BusinessRetailSubcategory: BaseEntity
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public Guid RetailSubcategoryId { get; set; }
        public RetailSubcategory RetailSubcategory { get; set; }
    }
}