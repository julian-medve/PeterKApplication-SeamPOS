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

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerPinPage : ContentPage
    {
        public OwnerPinPage()
        {
            InitializeComponent();
            
            BindingContext = DependencyService.Resolve<OwnerPinPageViewModel>();
        }
        
        private async void DialKeyboard_OnKeyPressed(object sender, DialKeyboardEventArgs e)
        {
            Console.WriteLine("Clicked:" + e.Clicked);
            var bc = BindingContext.As<OwnerPinPageViewModel>();
            if (await bc.AddPinEntry(e.Clicked))
            {
                Application.Current.MainPage = new OwnerMasterDetailPage();
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerPinPageViewModel>();
            bc.RemovePinEntry();
        }
    }
}