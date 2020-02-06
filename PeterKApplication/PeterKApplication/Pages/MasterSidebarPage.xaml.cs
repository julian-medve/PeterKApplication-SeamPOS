using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Pages.DpsTabbedPages;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterSidebarPage : ContentPage
    {
        public MasterSidebarPage()
        {
            InitializeComponent();
        }

        private void DpsModeClicked(object sender, HorizontalButtonEventArgs e)
        {
            Application.Current.MainPage = new DpsPinPage();
        }

        private void OffersClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Products,
                SubCategory = OwnerTabPageSubCategory.Offers
            });

            ApplicationHelper.Helper.Toggle.Execute(null);
        }

        private void AnalyticsClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Analytics
            });

            ApplicationHelper.Helper.Toggle.Execute(null);
        }

        private void SupportClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Support
            });

            ApplicationHelper.Helper.Toggle.Execute(null);
        }


        private void LocationsClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Locations
            }) ;

            ApplicationHelper.Helper.Toggle.Execute(null);
        }


        private void SettingsClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Business
            });

            ApplicationHelper.Helper.Toggle.Execute(null);
        }

        private void KnowledgeBaseClicked(object sender, HorizontalButtonEventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Support
            });

            ApplicationHelper.Helper.Toggle.Execute(null);
        }

        private void LogoutClicked(object sender, HorizontalButtonEventArgs e)
        {
            var authService = DependencyService.Resolve<AuthService>();

            authService.Logout();

            Application.Current.MainPage = new LoginPage();
        }

        private void OwnerModeClicked(object sender, HorizontalButtonEventArgs e)
        {
            Application.Current.MainPage = new OwnerMasterDetailPage();
        }
    }
}