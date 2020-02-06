using System;

namespace PeterKApplication.Shared.Models
{
    public class KnowledgeBase:BaseEntity
    {
        public ImageModel Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUri { get; set; }
    }
}