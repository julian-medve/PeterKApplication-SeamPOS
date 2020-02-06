using PeterKApplication.Controls;
using PeterKApplication.Pages.DpsTabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Extensions;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DpsPinPage : ContentPage
    {
        public DpsPinPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<DpsPinPageViewModel>();
        }

        private async void DialKeyboard_OnKeyPressed(object sender, DialKeyboardEventArgs e)
        {
            Console.WriteLine("Clicked:" + e.Clicked);
            var bc = BindingContext.As<DpsPinPageViewModel>();
            if (await bc.AddPinEntry(e.Clicked))
            {
                Application.Current.MainPage = new DpsMasterDetailPage();
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var bc = BindingContext.As<DpsPinPageViewModel>();
            bc.RemovePinEntry();
        }
    }
}