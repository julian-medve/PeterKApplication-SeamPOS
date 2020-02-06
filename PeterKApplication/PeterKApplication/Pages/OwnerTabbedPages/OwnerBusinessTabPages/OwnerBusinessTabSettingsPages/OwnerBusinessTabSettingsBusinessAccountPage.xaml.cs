using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSettingsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSettingsBusinessAccountPage : ContentPage
    {
        public OwnerBusinessTabSettingsBusinessAccountPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerBusinessTabSettingsBusinessAccountPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerBusinessTabSettingsBusinessAccountPageViewModel>().Initialize();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            await BindingContext.As<OwnerBusinessTabSettingsBusinessAccountPageViewModel>().Save();
            await Navigation.PopAsync();
        }

        private void CenterImageOrCirclePlus_OnImageChanged(object sender, CenterImageOrCirclePlusEventArgs e)
        {
            BindingContext.As<OwnerBusinessTabSettingsBusinessAccountPageViewModel>().SetImage(e.Image);
        }
    }
}