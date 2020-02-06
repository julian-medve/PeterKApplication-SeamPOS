using System;
using System.Collections.Generic;
using PeterKApplication.Extensions;
using PeterKApplication.ViewModels;
using Xamarin.Forms;

namespace PeterKApplication.Pages
{
    public partial class SignupSetup1Page : ContentPage
    {
        public SignupSetup1Page()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<SignupSetup1PageViewModel>();
        }

        private void OnOwnerSetupClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new OwnerWalkThroughPage();
        }

        private async void OnAgentSetupClicked(object sender, EventArgs e)
        {
            var bc = BindingContext.As<SignupSetup1PageViewModel>();

            if (await bc.SetAgentCode())
            {
                Application.Current.MainPage = new OwnerWalkThroughPage();
            }
        }
    }
}
