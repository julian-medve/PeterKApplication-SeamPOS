using System;
using System.Linq;
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
    public interface IMPesaService
    {
        Task ValidateTransaction(MPesaTransaction mPesaTransaction);
        Task<MPesaPaymentResDto> MPesaOrderPayment(MPesaPaymentReqDto mPesaOrderPaymentReq);
    }
    
    public class MPesaService : IMPesaService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public MPesaService(IConfiguration configuration, IAuthService authService, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _configuration = configuration;
            _authService = authService;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        
        public async Task ValidateTransaction(MPesaTransaction mPesaTransaction)
        {
            if (mPesaTransaction == null) return;
            
            await _dbContext.MPesaTransactions.AddAsync(mPesaTransaction);

            if (!string.IsNullOrEmpty(mPesaTransaction.TransId) && 
                !string.IsNullOrEmpty(mPesaTransaction.BillRefNumber))
            {
                
                var order = await _dbContext
                    .Orders
                    .FirstOrDefaultAsync(o => o.AppUserId + "-" + o.OrderNumber == mPesaTransaction.BillRefNumber);

                if (order != null)
                {
                    order.TransactionNumber = mPesaTransaction.TransId;
                    order.OrderStatus = OrderStatus.Paid;
                }
            }
            
            await _dbContext.SaveChangesAsync();
        }

        private async Task<MPesaToken> GetMPesaToken()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MPesaSettings:OAuth:BaseUrl"]);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",Convert.ToBase64String(
                    System.Text.Encoding.ASCII.GetBytes(
                        _configuration["MPesaSettings:OAuth:ConsumerKey"] + 
                        ":" + 
                        _configuration["MPesaSettings:OAuth:ConsumerSecret"])));

            var response = await client.PostAsJsonAsync("", "");
            if (!response.IsSuccessStatusCode)
            {
                throw new AppException("Unsuccessful call to MPesa OAuth API");
            }
            
            var jsonString = await response.Content.ReadAsStringAsync();
            
            try
            {
                // Validate missing fields of object
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                return JsonConvert.DeserializeObject<MPesaToken>(jsonString, settings);
            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }

        }

        public async Task<MPesaPaymentResDto> MPesaOrderPayment(MPesaPaymentReqDto mPesaOrderPaymentReq)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == mPesaOrderPaymentReq.OrderId);
            if (order == null)
            {
                throw new AppException("Order not found");
            }

            var token = await GetMPesaToken();
            
            var client = new HttpClient();
            client.BaseAddress = new Uri(_configuration["MPesaSettings:BaseUrl"]);
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", token.AccessToken);
            
            
            var paymentRequest = new MPesaPaymentRequest();
            
            var response = await client.PostAsJsonAsync("", paymentRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new AppException("Unsuccessful call to MPesa API");
            }
            
            var jsonString = await response.Content.ReadAsStringAsync();
              
            try
            {
                // Validate missing fields of object
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                var mPesaPaymentResponse = JsonConvert.DeserializeObject<MPesaPaymentResponse>(jsonString, settings);

                /*
                if (mPesaPaymentResponse.IsPaymentSuccessful)
                
                    order.OrderStatus = OrderStatus.Paid;
                    await _dbContext.SaveChangesAsync();
                */

                return new MPesaPaymentResDto();
            }
            catch (Exception e)
            {
                throw new AppException(e.Message);
            }
        }
    }
    
    public class MPesaToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }

    public class MPesaPaymentRequest
    {
        public string ValidationUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ResponseType { get; set; }
        public string ShortCode { get; set; }
    }

    public class MPesaPaymentResponse
    {
        public string ConversationId { get; set; }
        public string OriginatorConversationId { get; set; }
        public string ResponseDescription { get; set; }
    }
}