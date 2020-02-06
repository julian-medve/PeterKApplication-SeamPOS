using System;

namespace PeterKApplication.Shared.Models
{
    public class Sync:BaseEntity
    {
        public DateTime SyncedOn { get; set; }
        public long DataAmount { get; set; }
        
        public Guid? BusinessId { get; set; }
        public Business Business { get; set; }
    }
}