using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Exceptions;
using Newtonsoft.Json;

namespace PeterKApplication.Web.Services
{
    public interface IOwnerService
    {
        Task<AuthOwnerResDto> AuthenticateAsync(AuthOwnerReqDto authOwnerReq);
        Task<RegisterOwnerResDto> RegisterAsync(RegisterOwnerReqDto registerOwnerReq);
        Task<VerifyPhoneNumberResDto> VerifyPhoneNumber(VerifyPhoneNumberReqDto verifyPhoneNumberReq);
        Task<SmsConfirmationResDto> SendSmsConfirmationAsync(SmsConfirmationReqDto smsConfirmationReq);
        Task UpdateAsync(UpdateOwnerReqDto updateOwnerReq);
        Task UpdatePin(UpdateOwnerPinDto updateOwnerPin);

        Task<AgentDto> GetAgent();
        Task UpdateAgent(AgentDto agent);
    }

    public class SendMessageReq
    {
        public string ProfileId { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }

    public class SendMessageRes
    {
        [JsonProperty("request_id")] public string RequestId { get; set; }

        [JsonProperty("sms_units_balance")] public long SmsUnitsBalance { get; set; }

        [JsonProperty("message_length")] public long MessageLength { get; set; }

        [JsonProperty("sms_count")] public long SmsCount { get; set; }
    }

    public class OwnerService : IOwnerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public OwnerService(UserManager<AppUser> userManager, AppDbContext dbContext, IAuthService authService,
            ITokenService tokenService, IMailService mailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _authService = authService;
            _tokenService = tokenService;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<AuthOwnerResDto> AuthenticateAsync(AuthOwnerReqDto authOwnerReq)
        {
            var appUser = _dbContext.Users.SingleOrDefault(u => u.PhoneNumber == authOwnerReq.PhoneNumber);
            if (appUser == null)
            {
                throw new AppException("Invalid phone number and/or password");
            }

            if (!(await _userManager.IsInRoleAsync(appUser, UserRole.Owner)))
            {
                throw new AppException("Invalid phone number and/or password");
                ;
            }

            if (!(await _userManager.CheckPasswordAsync(appUser, authOwnerReq.Password)))
            {
                throw new AppException("Invalid phone number and/or password");
                ;
            }

            if (!appUser.PhoneNumberConfirmed)
            {
                throw new AppException("Phone number not confirmed");
            }

            return new AuthOwnerResDto
            {
                Token = await _tokenService.CreateToken(appUser)
            };
        }

        public async Task<RegisterOwnerResDto> RegisterAsync(RegisterOwnerReqDto registerOwnerReq)
        {
            var business = new Business
            {
                Name = registerOwnerReq.BusinessName
            };

            var appUser = new AppUser
            {
                FirstName = registerOwnerReq.FirstName,
                LastName = registerOwnerReq.LastName,
                UserName = registerOwnerReq.PhoneNumber,
                Email = registerOwnerReq.Email,
                CountryCode = registerOwnerReq.CountryCode,
                PhoneNumber = registerOwnerReq.PhoneNumber,
                Business = business
            };

            var createResult = await _userManager.CreateAsync(appUser, registerOwnerReq.Password);
            if (!createResult.Succeeded)
            {
                throw new AppException(createResult.ToString());
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, UserRole.Owner);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(appUser);
                throw new AppException(roleResult.ToString());
            }

            var smsConfirmationRes =
                await SendSmsConfirmationAsync(new SmsConfirmationReqDto {PhoneNumber = appUser.PhoneNumber});

            return new RegisterOwnerResDto
            {
                IsConfirmationCodeSent = smsConfirmationRes.IsConfirmationCodeSent
            };
        }

        public async Task<SmsConfirmationResDto> SendSmsConfirmationAsync(SmsConfirmationReqDto smsConfirmationReq)
        {
            var user = await _userManager.FindByNameAsync(smsConfirmationReq.PhoneNumber);
            if (user == null)
            {
                throw new AppException("User with given phone number not found");
            }

            if (user.PhoneNumberConfirmed)
            {
                throw new AppException("Phone number already confirmed");
            }

            var confirmationCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

            // TODO since it cannot be tested just print the confirmation number to console
            var isSent = await SendConfirmationSmsAsync(user, confirmationCode);

            return new SmsConfirmationResDto
            {
                IsConfirmationCodeSent = isSent,
                //TODO just for now, send confirmation code back in response
                ConfirmationCode = confirmationCode
            };
        }

        private async Task<bool> SendConfirmationSmsAsync(AppUser user, string message)
        {
            var client = new HttpClient {BaseAddress = new Uri(_configuration["SmsSettings:BaseUrl"])};
            client.DefaultRequestHeaders.Add("app-key", _configuration["SmsSettings:AppKey"]);
            client.DefaultRequestHeaders.Add("app-secret", _configuration["SmsSettings:AppSecret"]);

            var sendMessage = new SendMessageReq
            {
                ProfileId = "noIdea",
                PhoneNumber = user.PhoneNumber,
                Message = "Your confirmation code is " + message
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("api/sendSms", sendMessage);

            if (!response.IsSuccessStatusCode)
            {
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            try
            {
                // Validate missing fields of object
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.MissingMemberHandling = MissingMemberHandling.Error;

                var sendMessageRes = JsonConvert.DeserializeObject<SendMessageRes>(jsonString, settings);

                if (sendMessageRes.SmsCount < 1)
                {
                }
            }
            catch (Exception)
            {
            }

            return _mailService.SendMail(user, "SeamPos Confirmation Code",
                "Your SeamPos confirmation code: " + message);
        }

        public async Task<VerifyPhoneNumberResDto> VerifyPhoneNumber(VerifyPhoneNumberReqDto verifyPhoneNumberReq)
        {
            var user = await _userManager.FindByNameAsync(verifyPhoneNumberReq.PhoneNumber);
            if (user == null)
            {
                throw new AppException("User with given phone number does not exist");
            }

            if (user.PhoneNumberConfirmed)
            {
                throw new AppException("Phone number already confirmed");
            }

            var isPhoneConfirmed = await _userManager.VerifyChangePhoneNumberTokenAsync(
                user,
                verifyPhoneNumberReq.ConfirmationCode,
                user.PhoneNumber);

            if (!isPhoneConfirmed)
            {
                throw new AppException("Invalid confirmation code");
            }

            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);

            return new VerifyPhoneNumberResDto
            {
                IsPhoneNumberConfirmed = true,
                Token = await _tokenService.CreateToken(user)
            };
        }

        public async Task UpdateAsync(UpdateOwnerReqDto updateOwnerReq)
        {
            var currentUser = await _authService.CurrentUser();

            currentUser.FirstName = updateOwnerReq.FirstName;
            currentUser.LastName = updateOwnerReq.LastName;
            currentUser.Email = updateOwnerReq.Email;
            currentUser.CountryCode = updateOwnerReq.CountryCode;
            currentUser.PhoneNumber = updateOwnerReq.PhoneNumber;
            currentUser.IsAutoSyncEnabled = updateOwnerReq.IsAutoSyncEnabled;

            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                throw new AppException(updateResult.ToString());
            }
        }

        public async Task UpdatePin(UpdateOwnerPinDto updateOwnerPin)
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            if (!await _userManager.IsInRoleAsync(currentUser, UserRole.Owner))
            {
                throw new AppException("Not authorized");
            }

            currentUser.Pin = updateOwnerPin.Pin;
            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                throw new AppException(updateResult.ToString());
            }
        }

        // Agent methods

        public async Task<AgentDto> GetAgent()
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            return new AgentDto
            {
                AgentCode = currentUser.AgentCode
            };
        }

        public async Task UpdateAgent(AgentDto agent)
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            currentUser.AgentCode = agent.AgentCode;

            await _dbContext.SaveChangesAsync();
        }
    }
}