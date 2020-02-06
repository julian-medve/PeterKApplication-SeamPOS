using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Dtos;

namespace PeterKApplication.ViewModels
{
    public class DpsPinPageViewModel : INotifyPropertyChanged
    {
        private readonly PrivateApiService _privateApiService;
        private readonly AuthService _authService;
        private AuthStaffReqDto _authStaffReqDto = new AuthStaffReqDto();
        private ApiExecutionResponse<AuthStaffResDto> _authStaffResponse;
        private string _pin = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;

        public AuthStaffReqDto AuthStaffReqDto
        {
            get => _authStaffReqDto;
            set
            {
                _authStaffReqDto = value;
                OnPropertyChanged(nameof(AuthStaffReqDto));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DpsPinPageViewModel(PrivateApiService privateApiService, AuthService authService)
        {
            _privateApiService = privateApiService;
            _authService = authService;
        }

        public async Task<bool> LoginDps()
        {
            AuthStaffResponse = await ApiHelper.Execute(_privateApiService.Client.StaffLogin(AuthStaffReqDto));

            if (!AuthStaffResponse.HasError)
            {
                _authService.SetToken(AuthStaffResponse.Response.StaffToken);
                return true;
            }

            return false;
        }

        public ApiExecutionResponse<AuthStaffResDto> AuthStaffResponse
        {
            get => _authStaffResponse;
            set
            {
                _authStaffResponse = value;
                OnPropertyChanged(nameof(AuthStaffResponse));
            }
        }

        public async Task<bool> AddPinEntry(int entry)
        {
            if (Pin.Length < 4)
            {
                Pin += entry;

                if (Pin.Length == 4)
                {
                    return await LoginDps();
                }
            }

            return false;
        }

        public void RemovePinEntry()
        {
            if (Pin.Length > 0)
            {
                Pin = Pin.Remove(Pin.Length - 1);
            }
        }

        public string Pin
        {
            get => _pin;
            set
            {
                _pin = value;
                AuthStaffReqDto.Pin = Pin;
                OnPropertyChanged(nameof(Pin));
            }
        }

        public static DpsPinPageViewModel Self => null;

        public string CurrencyFormat => AuthService.CurrentUser().CurrencyFormat;
    }
}