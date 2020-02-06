using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerDashboardTabPage : ContentPage
    {
        private PillTabDefinition _pillTab1 = new PillTabDefinition{Selected = true};
        private PillTabDefinition _pillTab2 = new PillTabDefinition();
        private PillTabDefinition _pillTab3 = new PillTabDefinition();

        public OwnerDashboardTabPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerDashboardTabPageViewModel>();

            SwitchToTab(_pillTab1.Id);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bc = BindingContext.As<OwnerDashboardTabPageViewModel>();

            bc.Initialize();
        }

        private void SwitchToTab(Guid obj)
        {
            Console.WriteLine("Switch to guid:" +obj);
            var selectedT = new PillTabDefinition
            {
                Id = obj,
                Selected = true
            };

            Tab1Data.IsVisible = Tab2Data.IsVisible = Tab3Data.IsVisible = false;
            
            Tab1 = Tab1.Id == obj ? selectedT : new PillTabDefinition();
            Tab2 = Tab2.Id == obj ? selectedT : new PillTabDefinition();
            Tab3 = Tab3.Id == obj ? selectedT : new PillTabDefinition();

            if (Tab1.Selected) Tab1Data.IsVisible = true;
            if (Tab2.Selected) Tab2Data.IsVisible = true;
            if (Tab3.Selected) Tab3Data.IsVisible = true;
        }
        
        public PillTabDefinition Tab1
        {
            get => _pillTab1;
            set
            {
                _pillTab1 = value;
                OnPropertyChanged(nameof(Tab1));
            }
        }

        public PillTabDefinition Tab2
        {
            get => _pillTab2;
            set
            {
                _pillTab2 = value;
                OnPropertyChanged(nameof(Tab2));
            }
        }

        public PillTabDefinition Tab3
        {
            get => _pillTab3;
            set
            {
                _pillTab3 = value;
                OnPropertyChanged(nameof(Tab3));
            }
        }
        private void GoToTab3(object sender, EventArgs e)
        {
            SwitchToTab(_pillTab3.Id);
        }

        private void GoToTab2(object sender, EventArgs e)
        {
            SwitchToTab(_pillTab2.Id);
        }

        private void GoToTab1(object sender, EventArgs e)
        {
            SwitchToTab(_pillTab1.Id);
        }

        private void OnGoToProducts(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Products
            });
        }

        private void OnGoToStaff(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Staff
            });
        }

        private void OnGoToPayments(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ChangeTab", new ChangeOwnerTab
            {
                Page = OwnerTabPage.Business,
                SubCategory = OwnerTabPageSubCategory.Payments
            });
        }

        private void NavigationBar_OnLeftImageTapped(object sender, EventArgs e)
        {
            ApplicationHelper.ToggleMasterMenu();
        }
    }
}