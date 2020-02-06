using System;
using System.Collections.Generic;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class RetailSubcategory : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<BusinessRetailSubcategory> BusinessRetailSubcategories { get; set; }
    }
}