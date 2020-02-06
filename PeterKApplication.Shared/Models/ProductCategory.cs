using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        
        public Guid? BusinessId { get; set; }
        public Business Business { get; set; }
    }
}