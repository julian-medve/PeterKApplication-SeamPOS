using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabLocationsPages;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabLocationsPage : ContentPage
    {
        public OwnerBusinessTabLocationsPage()
        {
            InitializeComponent(); 
            BindingContext = DependencyService.Resolve<OwnerBusinessTabLocationsPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerBusinessTabLocationsPageViewModel>().LoadLocations();
        }

        private void LocationMemberClicked(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabLocationEditPage(e.OriginalObject as BusinessLocation));
        }

        private void NewLocationMemberClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabLocationEditPage(null));
        }
    }
}