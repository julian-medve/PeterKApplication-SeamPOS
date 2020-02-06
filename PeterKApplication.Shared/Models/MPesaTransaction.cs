using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PeterKApplication.Shared.Models
{
    public class MPesaTransaction
    {
        public Guid Id { get; set; }
        
        [Required, JsonProperty("TransID")]
        public string TransId { get; set; }

        public string TransactionType { get; set; }
        
        public string TransTime { get; set; }

        public string TransAmount { get; set; }

        public string BusinessShortCode { get; set; }

        public string BillRefNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string OrgAccountBalance { get; set; }

        [JsonProperty("ThirdPartyTransID")]
        public string ThirdPartyTransId { get; set; }

        [JsonProperty("MSISDN")]
        public string Msisdn { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Json { get; set; }
    }
}