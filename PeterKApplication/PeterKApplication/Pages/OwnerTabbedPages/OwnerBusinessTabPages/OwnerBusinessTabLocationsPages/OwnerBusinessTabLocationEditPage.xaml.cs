using PeterKApplication.Extensions;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabLocationsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabLocationEditPage : ContentPage
    {
        public OwnerBusinessTabLocationEditPage(BusinessLocation location)
        {
            InitializeComponent();

            /*DeleteButton.IsVisible = Disablebutton.IsVisible = member != null;*/

            BindingContext = DependencyService.Resolve<OwnerBusinessTabLocationEditPageViewModel>();
            BindingContext.As<OwnerBusinessTabLocationEditPageViewModel>().ExistingMember(location);
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            if (BindingContext.As<OwnerBusinessTabLocationEditPageViewModel>().Save())
            {
                await Navigation.PopAsync();
            }
            System.Diagnostics.Debug.Write("SaveClicked");
        }

        /*private void DeleteClicked(object sender, EventArgs e)
        {
            BindingContext.As<OwnerBusinessTabStaffEditProfilePageViewModel>().Delete();
            System.Diagnostics.Debug.Write("DeleteClicked");
        }*/

    }
}