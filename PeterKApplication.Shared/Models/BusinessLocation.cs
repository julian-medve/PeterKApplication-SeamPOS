using System;
using System.Collections.Generic;

namespace PeterKApplication.Shared.Models
{
    public class BusinessLocation : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Business> Businesses { get; set; }
    }
}