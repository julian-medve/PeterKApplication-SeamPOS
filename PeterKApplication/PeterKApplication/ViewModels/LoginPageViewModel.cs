using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Enums;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.ServiceInterfaces;
using PeterKApplication.Services;
using PeterKApplication.Shared.Dtos;
using Xamarin.Forms;

namespace PeterKApplication.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IInitializationService _initializationService;
        private readonly SyncService _syncService;
        private readonly AuthService _authService;
        private RegisterOwnerReqDto _registerOwnerReqDto = new RegisterOwnerReqDto();
        private AuthOwnerReqDto _authOwnerReqDto = new AuthOwnerReqDto();
        private ApiExecutionResponse<RegisterOwnerResDto> _registrationResponse;
        private ApiExecutionResponse<AuthOwnerResDto> _loginResponse;
        private object _phonePrefix;

        public static LoginPageViewModel Self => null;

        public RegisterOwnerReqDto RegisterOwnerReqDto
        {
            get => _registerOwnerReqDto;
            set
            {
                _registerOwnerReqDto = value;
                OnPropertyChanged(nameof(RegisterOwnerReqDto));
            }
        }

        public AuthOwnerReqDto AuthOwnerReqDto
        {
            get => _authOwnerReqDto;
            set
            {
                _authOwnerReqDto = value;
                OnPropertyChanged(nameof(AuthOwnerReqDto));
            }
        }

        public LoginPageViewModel(AuthService authService, IInitializationService initializationService, SyncService syncService)
        {
            _authService = authService;
            _initializationService = initializationService;
            _syncService = syncService;
        }

        public async Task Login()
        {
             LoginResponse = await _authService.Login(AuthOwnerReqDto);

            if (!LoginResponse.HasError)
            {
                await _syncService.SyncManual();
            }
        }

        public ApiExecutionResponse<AuthOwnerResDto> LoginResponse
        {
            get => _loginResponse;
            set
            {
                _loginResponse = value;
                OnPropertyChanged(nameof(LoginResponse));
            }
        }

        public async Task<bool> Register()
        {
            RegistrationResponse = await _authService.Register(new RegisterOwnerReqDto
            {
                Email = RegisterOwnerReqDto.Email,
                Password = RegisterOwnerReqDto.Password,
                BusinessName = RegisterOwnerReqDto.BusinessName,
                ConfirmPassword = RegisterOwnerReqDto.ConfirmPassword,
                CountryCode = RegisterOwnerReqDto.CountryCode,
                FirstName = RegisterOwnerReqDto.FirstName,
                LastName = RegisterOwnerReqDto.LastName,
                PhoneNumber = FullPhoneNumber
            });

            if (!RegistrationResponse.HasError)
            {
                return true;
            }

            return false;
        }

        public string FullPhoneNumber => PhonePrefix + RegisterOwnerReqDto.PhoneNumber;

        public ApiExecutionResponse<RegisterOwnerResDto> RegistrationResponse
        {
            get => _registrationResponse;
            set
            {
                _registrationResponse = value;
                OnPropertyChanged(nameof(RegistrationResponse));
            }
        }

        public List<string> CountryCodeOptions => new List<string>
        {
            "KE",
            "HR"
        };

        public List<string> PhoneNumberPrefixOptions => new List<string>
        {
            "+254",
            "+385"
        };

        public object PhonePrefix
        {
            get => _phonePrefix;
            set
            {
                _phonePrefix = value;
                OnPropertyChanged(nameof(PhonePrefix));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LoginStatus GetLoginStatus()
        {
            if (LoginResponse?.GeneralError?.Equals("Phone number not confirmed") == true)
            {
                return LoginStatus.OwnerNotVerified;
            }
            
            return _authService.GetLoginStatus();
        }

        public void Initialize()
        {
            _initializationService.SetStatusbarColor(StaticResourceHelper.Get<Color>("LightDarkGrayColor"));
        }

        public void ResetStatusBarColor()
        {
            _initializationService.SetStatusbarColor(Color.White);
        }
    }
}