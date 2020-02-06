using System;
using System.Collections.Generic;
using System.Linq;
using PeterKApplication.Controls;
using PeterKApplication.Enums;
using PeterKApplication.Extensions;
using PeterKApplication.Pages.DpsTabbedPages;
using PeterKApplication.Pages.OwnerTabbedPages;
using PeterKApplication.ViewModels;
using Xamarin.Forms;

namespace PeterKApplication.Pages
{
    public partial class SmsConfirmationPage : ContentPage
    {
        public SmsConfirmationPage(string phoneNumber)
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<SmsConfirmationPageViewModel>();

            BindingContext.As<SmsConfirmationPageViewModel>().PhoneNumber = phoneNumber;
        }

        public async void OnDialKeyPressed(object sender, DialKeyboardEventArgs eventArgs)
        {
            var bc = BindingContext.As<SmsConfirmationPageViewModel>();
            
            bc.Input += eventArgs.Clicked;

            if (bc.Input.Length == 6)
            {
                switch(await bc.ConfirmCode())
                {
                    case LoginStatus.Dps:
                        Application.Current.MainPage = new DpsMasterDetailPage();
                        break;
                    case LoginStatus.Owner:
                        Application.Current.MainPage = new OwnerMasterDetailPage();
                        break;
                    case LoginStatus.OwnerNotSetup:
                        Application.Current.MainPage = new SignupSetup1Page();
                        break;
                }
            }
        }
        
        private async void AppButton_OnOnClicked(object sender, EventArgs e)
        {
            await BindingContext.As<SmsConfirmationPageViewModel>().ResendConfirmationCode();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var bc = BindingContext.As<SmsConfirmationPageViewModel>();
            bc.Input = bc.Input.Remove(bc.Input.Length - 1);
        }
    }
}
