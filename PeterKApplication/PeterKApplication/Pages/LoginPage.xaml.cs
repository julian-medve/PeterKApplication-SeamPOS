using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeterKApplication.Enums;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Pages.DpsTabbedPages;
using PeterKApplication.Pages.OwnerTabbedPages;
using PeterKApplication.ServiceInterfaces;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private List<TabDefinition> _tabs;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<LoginPageViewModel>();
            
            Tabs = new List<TabDefinition>
            {
                new TabDefinition
                {
                    Title = "LOGIN",
                    Element = Tab1,
                    IsSelected = true,
                    Id = "LOGIN"
                },
                new TabDefinition
                {
                    Title = "SIGN UP",
                    Element = Tab2,
                    Id = "SIGN UP"
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext.As<LoginPageViewModel>().Initialize();
        }

        public List<TabDefinition> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var bc = BindingContext.As<LoginPageViewModel>();
            
            await bc.Login();

            if (bc.GetLoginStatus() != LoginStatus.NotLoggedIn)
            {
                bc.ResetStatusBarColor();
            }
            
            switch (bc.GetLoginStatus())
            {
                case LoginStatus.Dps:
                    Application.Current.MainPage = new DpsPinPage();
                    break;
                case LoginStatus.Owner:
                    Application.Current.MainPage = new SignupSetup2Page();
                    break;
                case LoginStatus.NotLoggedIn:
                    Console.WriteLine("Nothing to do, some error occurred");
                    break;
                case LoginStatus.OwnerNotSetup:
                    Application.Current.MainPage = new SignupSetup1Page();
                    break;
                case LoginStatus.OwnerNotVerified:
                    Application.Current.MainPage = new SmsConfirmationPage(bc.AuthOwnerReqDto.PhoneNumber);
                    break;
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var bc = BindingContext.As<LoginPageViewModel>();
            if (await bc.Register())
            {
                Application.Current.MainPage = new SmsConfirmationPage(bc.FullPhoneNumber);
            }
        }
    }
}