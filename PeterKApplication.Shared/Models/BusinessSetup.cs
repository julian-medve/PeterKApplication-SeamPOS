using System;
using System.Collections.Generic;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class BusinessSetup : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Business> Businesses { get; set; }
    }
}