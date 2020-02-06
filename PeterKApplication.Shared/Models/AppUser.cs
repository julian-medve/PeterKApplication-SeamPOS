using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PeterKApplication.Shared.Models
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        [Key]
        public override string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string Pin { get; set; }
        public byte[] Image { get; set; }
        public bool IsAutoSyncEnabled { get; set; }
        public string AgentCode { get; set; }

        public Guid? BusinessLocationId { get; set; }

        public Guid? BusinessId { get; set; }
        public Business Business { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSynced { get; set; }
        public ICollection<Order> Orders { get; set; }

        public string CurrencyFormat { get; set; }
        public float Tax { get; set; }
    }
}