using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabStaffPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabStaffEditProfilePage : ContentPage
    {
        public OwnerBusinessTabStaffEditProfilePage(AppUser member)
        {
            InitializeComponent();

            DeleteButton.IsVisible = Disablebutton.IsVisible = member != null;
            
            BindingContext = DependencyService.Resolve<OwnerBusinessTabStaffEditProfilePageViewModel>();
            BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().ExistingMember(member);
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            if (BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().Save())
            {
                await Navigation.PopAsync();
            }
            System.Diagnostics.Debug.Write("SaveClicked");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().Delete();
            System.Diagnostics.Debug.Write("DeleteClicked");
        }

        private void DisableClicked(object sender, EventArgs e)
        {
            BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().Disable();
            System.Diagnostics.Debug.Write("DisableClicked");
        }

        private void CenterImageOrCirclePlus_OnImageChanged(object sender, CenterImageOrCirclePlusEventArgs e)
        {
            BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().SetStaffMemberImage(e.Image);
            System.Diagnostics.Debug.Write("CenterImageOrCirclePlus_OnImageChanged");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var bc = BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>();
            bc.Initialize();
        }
    }
}