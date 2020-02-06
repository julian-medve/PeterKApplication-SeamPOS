using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Namotion.Reflection;
using Newtonsoft.Json;
using PeterKApplication.Data;
using PeterKApplication.Enums;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using Xamarin.Essentials;

namespace PeterKApplication.Services
{
    public class AuthService
    {
        private readonly PrivateApiService _privateApiService;
        private readonly PublicApiService _publicApiService;
        private static JwtSecurityToken _token;

        public static bool IsOwner { get; set; }
        public static bool OwnerSetupCompleted { get; set; }
        public static bool IsOwnerVerified { get; set; }

        public static AppUser CurrentUser()
        {
            using (var db = new LocalDbContext())
            {
                if (_token.Payload.TryGetValue("email", out var email))
                    return db.Users.First(u => u.Email.ToLower().Equals(((string)email).ToLower()));
            }

            return null;
        }

        public static string PhoneNumber { get; set; }
        public static bool ShouldAutoSync { get; set; }
        public static Guid? BusinessId { get; set; }
        public static string UserId { get; set; }

        public AuthService(PrivateApiService privateApiService, PublicApiService publicApiService)
        {
            _privateApiService = privateApiService;
            _publicApiService = publicApiService;
        }

        public bool IsLoggedIn()
        {
            return HydrateToken();
        }

        private bool HydrateToken()
        {
            var tokenExists = Preferences.ContainsKey("Token");

            if (tokenExists)
            {
                var tokenInStorage = Preferences.Get("Token", null);
                var tokenHandler = new JwtSecurityTokenHandler();
                _token = tokenHandler.ReadJwtToken(tokenInStorage);
                if (_token.Payload.TryGetValue("role", out var role))
                {
                    IsOwner = Equals(role, "OWNER");
                }

                if (_token.Payload.TryGetValue("IsAdded", out var setupDone))
                {
                    OwnerSetupCompleted = setupDone.Equals("True");
                }

                if (_token.Payload.TryGetValue("IsPhoneConfirmed", out var verified))
                {
                    IsOwnerVerified = verified.Equals("True");
                }

                if (_token.Payload.TryGetValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone",
                    out var phone))
                {
                    PhoneNumber = (string)phone;
                }

                if (_token.Payload.TryGetValue("IsAutoSyncEnabled", out var sync))
                {
                    ShouldAutoSync = sync.Equals("True");
                }

                if (_token.Payload.TryGetValue("BusinessId", out var g))
                {
                    BusinessId = Guid.Parse((string)g);
                }

                if (_token.Payload.TryGetValue("UserId", out var id))
                {
                    UserId = (string)id;
                }
            }

            return tokenExists;
        }

        public async Task RefreshToken()
        {
            var res = await ApiHelper.Execute(_privateApiService.Client.RefreshToken());
            if (!res.HasError)
            {
                SetToken(res.Response.Token);
            }
        }

        public async Task<ApiExecutionResponse<AuthOwnerResDto>> Login(AuthOwnerReqDto req)
        {
            var loginRes = await ApiHelper.Execute(_publicApiService.Client.OwnerAuthentication(req));

            if (!loginRes.HasError)
            {
                SetToken(loginRes.Response.Token);
            }

            return loginRes;
        }

        public void SetToken(string responseToken)
        {
            Preferences.Set("Token", responseToken);
            HydrateToken();
        }

        public async Task<ApiExecutionResponse<RegisterOwnerResDto>> Register(RegisterOwnerReqDto req)
        {
            return await ApiHelper.Execute(_publicApiService.Client.OwnerRegister(req));
        }

        public LoginStatus GetLoginStatus()
        {
            if (IsLoggedIn())
            {
                if (IsOwner)
                {
                    if (!IsOwnerVerified)
                    {
                        return LoginStatus.OwnerNotVerified;
                    }

                    if (OwnerSetupCompleted)
                    {
                        return LoginStatus.Owner;
                    }

                    return LoginStatus.OwnerNotSetup;
                }

                return LoginStatus.Dps;
            }

            return LoginStatus.NotLoggedIn;
        }


        public bool AutoSync()
        {
            if (_token != null)
            {
                var shouldSync = ShouldAutoSync;

                if (!shouldSync) shouldSync = Preferences.Get("AutoSync", false);

                return shouldSync;
            }

            return false;
        }

        public void Logout()
        {
            using (UserDialogs.Instance.Loading("Logging you out"))
            {
                using var db = new LocalDbContext();
                Preferences.Clear();
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            }
        }
    }
}