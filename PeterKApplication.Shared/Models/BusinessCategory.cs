using System;
using System.Collections.Generic;

namespace PeterKApplication.Shared.Models
{
    public class BusinessCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<BusinessBusinessCategory> BusinessBusinessCategories { get; set; }
    }
}