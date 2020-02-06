using System.Threading.Tasks;
using PeterKApplication.Enums;
using PeterKApplication.Services;

namespace PeterKApplication.ViewModels
{
    public class LandingPageViewModel
    {
        private readonly AuthService _authService;
        private readonly PublicApiService _publicApiService;
        private readonly SyncService _syncService;

        public LandingPageViewModel(AuthService authService, PublicApiService publicApiService, SyncService syncService)
        {
            _authService = authService;
            _publicApiService = publicApiService;
            _syncService = syncService;
        }

        public bool IsLoggedIn()
        {
            return _authService.IsLoggedIn();
        }

        public async Task RefreshToken()
        {
            await _authService.RefreshToken();
        }

        public bool IsOwner()
        {
            return AuthService.IsOwner;
        }

        public LoginStatus GetLoginStatus()
        {
            return _authService.GetLoginStatus();
        }

        public string GetPhoneNumber()
        {
            return AuthService.PhoneNumber;
        }
    }
}