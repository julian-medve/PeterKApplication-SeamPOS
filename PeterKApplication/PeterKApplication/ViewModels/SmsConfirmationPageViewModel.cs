using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Enums;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Dtos;

namespace PeterKApplication.ViewModels
{
    public class SmsConfirmationPageViewModel: INotifyPropertyChanged
    {
        private readonly PublicApiService _publicApiService;
        private readonly AuthService _authService;
        private ApiExecutionResponse<VerifyPhoneNumberResDto> _confirmCodeResponse;
        private ApiExecutionResponse<SmsConfirmationResDto> _resendConfirmationCodeResponse;

        private string _input = string.Empty;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
                OnPropertyChanged(nameof(DialProgress));
            }
        }
        public int DialProgress => Input?.Length ?? 0;

        public SmsConfirmationPageViewModel(PublicApiService publicApiService, AuthService authService)
        {
            _publicApiService = publicApiService;
            _authService = authService;
        }

        public string PhoneNumber { get; set; }

        public async Task ResendConfirmationCode()
        {
            ResendConfirmationCodeResponse = await ApiHelper.Execute(
                _publicApiService.Client.ResendOwnerConfirmationCode(new SmsConfirmationReqDto
                {
                    PhoneNumber = PhoneNumber
                }));
        }

        public ApiExecutionResponse<SmsConfirmationResDto> ResendConfirmationCodeResponse
        {
            get => _resendConfirmationCodeResponse;
            set
            {
                _resendConfirmationCodeResponse = value;
                OnPropertyChanged(nameof(ResendConfirmationCodeResponse));
            }
        }

        public async Task<LoginStatus> ConfirmCode()
        {
            ConfirmCodeResponse = await ApiHelper.Execute(_publicApiService.Client.VerifyPhoneNumber(
                new VerifyPhoneNumberReqDto
                {
                    ConfirmationCode = Input,
                    PhoneNumber = PhoneNumber
                }));

            if (!ConfirmCodeResponse.HasError)
            {
                _authService.SetToken(ConfirmCodeResponse.Response.Token);
            }

            if (AuthService.IsOwnerVerified)
            {
                if (!AuthService.OwnerSetupCompleted)
                {
                    return LoginStatus.OwnerNotSetup;
                }
                
                if(AuthService.IsOwner)
                {
                    return LoginStatus.Owner;
                }

                return LoginStatus.Dps;
            }

            return LoginStatus.OwnerNotVerified;
        }

        public ApiExecutionResponse<VerifyPhoneNumberResDto> ConfirmCodeResponse
        {
            get => _confirmCodeResponse;
            set
            {
                _confirmCodeResponse = value;
                OnPropertyChanged(nameof(ConfirmCodeResponse));
            }
        }

        public static SmsConfirmationPageViewModel Self => null;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}