using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerOrdersTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerOrdersTabOrderDetailsPage : ContentPage
    {
        public OwnerOrdersTabOrderDetailsPage(Order eOrder)
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerOrdersTabOrderDetailsPageViewModel>();

            BindingContext.As<OwnerOrdersTabOrderDetailsPageViewModel>().Order = eOrder;
        }

        private void AppButton_SupportClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Support
            });
        }

        private void AppButton_FaqClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Faq
            });
        }
    }
}