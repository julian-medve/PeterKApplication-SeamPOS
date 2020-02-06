using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Dtos
{
    public class UpdateBusinessDto
    {
        public byte[] OwnersDocumentImage { get; set; }
        
        public byte[] BusinessDocumentImage { get; set; }
        
        public double? Longitude { get; set; }
        
        public double? Latitude { get; set; }
        
        public Guid? BusinessLocation { get; set; }
        
        public Guid? BusinessSetup { get; set; }
        
        public byte[] Image { get; set; }
        
        public ICollection<Guid> BusinessBusinessCategories { get; set; }
        public ICollection<Guid> BusinessRetailSubcategories { get; set; }
    }
}