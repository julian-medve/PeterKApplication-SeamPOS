using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupSetup2Page : ContentPage
    {
        public SignupSetup2Page()
        {
            InitializeComponent();
        }

        private void OnOwnerRolePicked(object sender, EventArgs e)
        {
            if (AuthService.OwnerSetupCompleted)
            {
                Application.Current.MainPage = new OwnerMasterDetailPage();
            }
            else
            {
                Application.Current.MainPage = new OwnerWalkThroughPage();
            }
        }

        private void OnDpsRolePicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new DpsPinPage();
        }
    }
}