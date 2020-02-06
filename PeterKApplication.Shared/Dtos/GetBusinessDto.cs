using System.Collections.Generic;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Dtos
{
    public class GetBusinessDto
    {
        public ImageModel OwnersDocumentImage { get; set; }
        
        public ImageModel BusinessDocumentImage { get; set; }
        
        public double? Longitude { get; set; }
        
        public double? Latitude { get; set; }
        
        public BusinessLocation BusinessLocation { get; set; }
        
        public BusinessSetup BusinessSetup { get; set; }
        
        public ImageModel Image { get; set; }
        
        public ICollection<BusinessBusinessCategory> BusinessBusinessCategories { get; set; }
        
        public ICollection<BusinessRetailSubcategory> BusinessRetailSubcategories { get; set; }
    }
}