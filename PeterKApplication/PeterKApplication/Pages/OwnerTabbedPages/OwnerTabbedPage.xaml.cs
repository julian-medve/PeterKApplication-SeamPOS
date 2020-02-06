using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerOrdersTabPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerTabbedPage : TabbedPage
    {
        public OwnerTabbedPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<OwnerDashboardTabPage, ChangeOwnerTab>(this, "ChangeTab", Callback);
            MessagingCenter.Subscribe<OwnerOrdersTabOrderDetailsPage, ChangeOwnerTab>(this, "ChangeTab", Callback);
            MessagingCenter.Subscribe<MasterSidebarPage, ChangeOwnerTab>(this, "ChangeTab", Callback);
        }

        private void Callback(Page arg1, ChangeOwnerTab change)
        {
            CurrentPage = Children[(int) change.Page];

            MessagingCenter.Send(this, "GoToSubCategory", change.SubCategory);
        }
    }
}