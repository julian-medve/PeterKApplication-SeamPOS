using System;
using System.Collections.Generic;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Models
{
    public class Business : BaseEntity
    {
        public string Name { get; set; }
        public ImageModel OwnersDocumentImage { get; set; }
        public ImageModel BusinessDocumentImage { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
        public BusinessSetup BusinessSetup { get; set; }
        public ImageModel Image { get; set; }
        public bool IsAdded { get; set; }
        public bool IsVerified { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
        public string AliasId { get; set; }
        
        public ICollection<BusinessBusinessCategory> BusinessBusinessCategories { get; set; }
        public ICollection<BusinessPaymentType> BusinessPaymentTypes { get; set; }
        public ICollection<BusinessRetailSubcategory> BusinessRetailSubcategories { get; set; }

        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Sync> Syncs { get; set; }
    }
}