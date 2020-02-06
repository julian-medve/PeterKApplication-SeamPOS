using PeterKApplication.Extensions;
using PeterKApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSettingsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSettingsBusinessCurrencyPage : ContentPage
    {
        public OwnerBusinessTabSettingsBusinessCurrencyPage()
        {
            InitializeComponent();

            try
            {
                BindingContext = DependencyService.Resolve<OwnerBusinessTabSettingsBusinessCurrencyPageViewModel>();
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.Write("Exception : " + e.ToString());
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerBusinessTabSettingsBusinessCurrencyPageViewModel>().Initialize();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            await BindingContext.As<OwnerBusinessTabSettingsBusinessCurrencyPageViewModel>().Save();
            await Navigation.PopAsync();
        }
    }
}