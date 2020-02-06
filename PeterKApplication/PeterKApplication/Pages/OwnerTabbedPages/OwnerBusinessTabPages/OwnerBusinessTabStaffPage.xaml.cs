using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabStaffPages;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabStaffPage : ContentPage
    {
        public OwnerBusinessTabStaffPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerBusinessTabStaffPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerBusinessTabStaffPageViewModel>().LoadStaff();
        }

        private void StaffMemberClicked(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabStaffEditProfilePage(e.OriginalObject as AppUser));
        }

        private void NewStaffMemberClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabStaffEditProfilePage(null));
        }
    }
}