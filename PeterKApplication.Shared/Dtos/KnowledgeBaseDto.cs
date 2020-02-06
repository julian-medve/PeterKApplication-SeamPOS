using System;
using System.ComponentModel.DataAnnotations.Schema;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Shared.Dtos
{
    public class KnowledgeBaseDto
    {
        public Guid? Id { get; set; }
        
        public ImageModel Image { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public string VideoUri { get; set; }

        [NotMapped]
        public bool IsVisible { get; set; }

        [NotMapped]
        public string HeaderIcon => IsVisible ? "-" : "+";
    }
}