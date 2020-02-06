using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface IVisaService
    {
        Task<VisaPaymentResDto> MerchantPushPayment(VisaPaymentReqDto visaPaymentReq);
    }
    
    public class VisaService : IVisaService
    {
         private readonly IConfiguration _configuration;
         private readonly IAuthService _authService;
         private readonly UserManager<AppUser> _userManager;
         private readonly AppDbContext _dbContext;

         public VisaService(IConfiguration configuration, IAuthService authService, UserManager<AppUser> userManager, 
             AppDbContext dbContext)
         {
             _configuration = configuration;
             _authService = authService;
             _userManager = userManager;
             _dbContext = dbContext;
         }

        public async Task<VisaPaymentResDto> MerchantPushPayment(VisaPaymentReqDto visaPaymentReq)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == visaPaymentReq.OrderId);
            if (order == null)
            {
                throw new AppException("Order not found");
            }
            
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["VisaSettings:BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic",Convert.ToBase64String(
                    System.Text.Encoding.ASCII.GetBytes(
                        _configuration["VisaSettings:Username"] + 
                           ":" + 
                           _configuration["VisaSettings:Password"])));
            
            
            var pushPaymentRequest = new MerchantPushPaymentRequest
            {
                LocalTransactionDateTime = DateTime.Now,
                AcquiringBin = _configuration["VisaSettings:AcquiringBin"],
                BusinessApplicationId = _configuration["VisaSettings:BusinessApplicationId"],
                MerchantCategoryCode = _configuration["VisaSettings:MerchantCategoryCode"],
                AcquirerCountryCode = _configuration["VisaSettings:AcquirerCountryCode"],
                RetrievalReferenceNumber = _configuration["VisaSettings:RetrievalReferenceNumber"],
                Amount = visaPaymentReq.Amount,
                RecipientPrimaryAccountNumber = _configuration["VisaSettings:RecipientPrimaryAccountNumber"],
                SystemsTraceAuditNumber = "6character",
                SenderAccountNumber = visaPaymentReq.SenderAccountNumber,
                TransactionCurrencyCode = _configuration["VisaSettings:TransactionCurrencyCode"],
                SenderName = visaPaymentReq.SenderName,
                CardAcceptor = new CardAcceptor
                {
                    IdCode = _configuration["VisaSettings:CardAcceptor:IdCode"],
                    Name = _configuration["VisaSettings:CardAcceptor:Name"]
                }
            };
            
            var response = await client.PostAsJsonAsync("merchantpushpayments",pushPaymentRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new AppException("Unsuccessful call to Visa API");
            }
            
            var jsonString = await response.Content.ReadAsStringAsync();
              
            try
            {
                // Validate missing fields of object
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                var visaPaymentResponse = JsonConvert.DeserializeObject<MerchantPushPaymentResponse>(jsonString, settings);

                if (visaPaymentResponse.ActionCode == "00")
                {
                    order.OrderStatus = OrderStatus.Paid;
                    await _dbContext.SaveChangesAsync();
                }

                return new VisaPaymentResDto
                {
                    TransactionIdentifier = visaPaymentResponse.TransactionIdentifier,
                    ActionCode = visaPaymentResponse.ActionCode,
                    ApprovalCode = visaPaymentResponse.ApprovalCode,
                    ResponseCode = visaPaymentResponse.ResponseCode
                };
            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }
        }
    }
    
    public class MerchantPushPaymentRequest
    {
        [JsonProperty("senderAccountNumber")]
        public string SenderAccountNumber { get; set; }

        [JsonProperty("localTransactionDateTime")]
        public DateTimeOffset LocalTransactionDateTime { get; set; }

        [JsonProperty("purchaseIdentifier")]
        public PurchaseIdentifier PurchaseIdentifier { get; set; }

        [JsonProperty("merchantCategoryCode")]
        public string MerchantCategoryCode { get; set; }

        [JsonProperty("feeProgramIndicator")]
        public string FeeProgramIndicator { get; set; }

        [JsonProperty("transactionCurrencyCode")]
        public string TransactionCurrencyCode { get; set; }

        [JsonProperty("acquiringBin")]
        public string AcquiringBin { get; set; }

        [JsonProperty("acquirerCountryCode")]
        public string AcquirerCountryCode { get; set; }

        [JsonProperty("retrievalReferenceNumber")]
        public string RetrievalReferenceNumber { get; set; }

        [JsonProperty("senderReference")]
        public string SenderReference { get; set; }

        [JsonProperty("secondaryId")]
        public string SecondaryId { get; set; }

        [JsonProperty("cardAcceptor")]
        public CardAcceptor CardAcceptor { get; set; }

        [JsonProperty("recipientPrimaryAccountNumber")]
        public string RecipientPrimaryAccountNumber { get; set; }

        [JsonProperty("systemsTraceAuditNumber")]
        public string SystemsTraceAuditNumber { get; set; }

        [JsonProperty("businessApplicationId")]
        public string BusinessApplicationId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("settlementServiceIndicator")]
        public string SettlementServiceIndicator { get; set; }
    }
    
    public class MerchantPushPaymentResponse
    {
        [JsonProperty("transactionIdentifier")]
        public long TransactionIdentifier { get; set; }

        [JsonProperty("actionCode")]
        public string ActionCode { get; set; }

        [JsonProperty("approvalCode")]
        public string ApprovalCode { get; set; }

        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("transmissionDateTime")]
        public DateTimeOffset TransmissionDateTime { get; set; }

        [JsonProperty("retrievalReferenceNumber")]
        public string RetrievalReferenceNumber { get; set; }

        [JsonProperty("settlementFlags")]
        public SettlementFlags SettlementFlags { get; set; }

        [JsonProperty("purchaseIdentifier")]
        public PurchaseIdentifier PurchaseIdentifier { get; set; }

        [JsonProperty("feeProgramIndicator")]
        public string FeeProgramIndicator { get; set; }

        [JsonProperty("merchantCategoryCode")]
        public long MerchantCategoryCode { get; set; }

        [JsonProperty("cardAcceptor")]
        public CardAcceptor CardAcceptor { get; set; }

        [JsonProperty("merchantVerificationValue")]
        public string MerchantVerificationValue { get; set; }
    }
    
    public partial class SettlementFlags
    {
        [JsonProperty("settlementResponsibilityFlag")]
        public string SettlementResponsibilityFlag { get; set; }

        [JsonProperty("givPreviouslyUpdatedFlag")]
        public string GivPreviouslyUpdatedFlag { get; set; }

        [JsonProperty("givUpdatedFlag")]
        public string GivUpdatedFlag { get; set; }

        [JsonProperty("settlementServiceFlag")]
        public string SettlementServiceFlag { get; set; }
    }

    public partial class CardAcceptor
    {
        [JsonProperty("idCode")]
        public string IdCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }

    public partial class PurchaseIdentifier
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }
    }
}