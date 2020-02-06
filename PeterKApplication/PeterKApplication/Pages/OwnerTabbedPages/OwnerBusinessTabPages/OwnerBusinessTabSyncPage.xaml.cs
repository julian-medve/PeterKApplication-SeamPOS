using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PeterKApplication.Extensions;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSyncPage : ContentPage
    {
        public OwnerBusinessTabSyncPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerBusinessTabSyncPageViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerBusinessTabSyncPageViewModel>().Initialize();
        }

        private async void StartManualSync(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerBusinessTabSyncPageViewModel>();
            
            using (UserDialogs.Instance.Loading("Syncing..."))
            {
                await bc.StartManualSync();
            }
        }
    }
}