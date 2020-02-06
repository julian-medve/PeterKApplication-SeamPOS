using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSupportPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSupportPage : ContentPage
    {
        public OwnerBusinessTabSupportPage()
        {
            InitializeComponent();
        }

        private void GoToKnowledgeBase(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSupportKnowledgeBasePage());
        }

        private void GoToMessageCustomerCare(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSupportMessageSupport());
        }

        private void GoToPlans(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSupportPlansPage());
        }
    }
}