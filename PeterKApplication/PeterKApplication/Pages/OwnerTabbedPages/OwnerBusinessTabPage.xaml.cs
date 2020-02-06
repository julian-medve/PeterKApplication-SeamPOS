using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabPage : ContentPage
    {
        public OwnerBusinessTabPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<OwnerTabbedPage, OwnerTabPageSubCategory>(this, "GoToSubCategory",
                (page, category) =>
                {
                    switch (category)
                    {
                        case OwnerTabPageSubCategory.Business:
                            break;
                        case OwnerTabPageSubCategory.Payments:
                            GoToPayments(null, null);
                            break;
                        case OwnerTabPageSubCategory.Staff:
                            GoToStaff(null, null);
                            break;
                        case OwnerTabPageSubCategory.Analytics:
                            GoToAnalytics(null, null);
                            break;
                        case OwnerTabPageSubCategory.Locations:
                            GotoLocations(null, null);
                            break;

                        case OwnerTabPageSubCategory.Support:
                        case OwnerTabPageSubCategory.Faq:
                            GoToSupport(null, null);
                            break;

                    }
                });
        }

        private void GoToSync(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSyncPage());
        }

        private void GoToPayments(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabPaymentsPage());
        }

        private void GoToStaff(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabStaffPage());
        }

        private void GoToAnalytics(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabAnalyticsPage());
        }

        private void GotoLocations(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabLocationsPage());
        }

        private void GoToSettings(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSettingsPage());
        }

        private void GoToSupport(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSupportPage());
        }

        private void HorizontalButton_OnClicked(object sender, Controls.HorizontalButtonEventArgs e)
        {

        }
    }
}