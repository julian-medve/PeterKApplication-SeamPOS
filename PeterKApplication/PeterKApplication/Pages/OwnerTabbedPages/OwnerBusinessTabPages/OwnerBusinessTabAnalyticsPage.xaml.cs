using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabAnalyticsPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabAnalyticsPage : ContentPage
    {
        private PillTabDefinition _pillTab1 = new PillTabDefinition{Selected = true};
        private PillTabDefinition _pillTab2 = new PillTabDefinition();
        private PillTabDefinition _pillTab3 = new PillTabDefinition();

        public OwnerBusinessTabAnalyticsPage()
        {
            InitializeComponent();
        }

        private void SwitchToTab(Guid obj)
        {
            Console.WriteLine("Switch to guid:" +obj);
            var selectedT = new PillTabDefinition
            {
                Id = obj,
                Selected = true
            };
            Tab1 = Tab1.Id == obj ? selectedT : new PillTabDefinition();
            Tab2 = Tab2.Id == obj ? selectedT : new PillTabDefinition();
            Tab3 = Tab3.Id == obj ? selectedT : new PillTabDefinition();
        }

        private void GoToAnalyticsReports(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabAnalyticsReportsPage());
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
    }
}