using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerOrdersTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerOrdersTabOrdersListingPage : ContentPage
    {
        public OwnerOrdersTabOrdersListingPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerOrdersTabOrdersListingPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerOrdersTabOrdersListingPageViewModel>().Initialize();
        }

        private void OrderList_OnDetailsClicked(object sender, OrderListDetailsEventArgs e)
        {
            Navigation.PushAsync(new OwnerOrdersTabOrderDetailsPage(e.Order));
        }
    }
}