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
    public partial class MasterDpsSidebarPage : ContentPage
    {
        public MasterDpsSidebarPage()
        {
            InitializeComponent();
        }

        private void DpsModeClicked(object sender, HorizontalButtonEventArgs e)
        {
            Application.Current.MainPage = new DpsMasterDetailPage();
        }

        private void LogoutClicked(object sender, HorizontalButtonEventArgs e)
        {
            var authService = DependencyService.Resolve<AuthService>();

            authService.Logout();

            Application.Current.MainPage = new LoginPage();
        }

        private void OwnerModeClicked(object sender, HorizontalButtonEventArgs e)
        {
            Application.Current.MainPage = new OwnerPinPage();
        }
    }
}