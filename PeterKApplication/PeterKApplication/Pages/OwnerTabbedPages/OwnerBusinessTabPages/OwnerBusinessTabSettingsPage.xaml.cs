using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSettingsPages;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSettingsPage : ContentPage
    {
        /*private List<TabDefinition> _tabs;*/

        public OwnerBusinessTabSettingsPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerBusinessTabSettingsPageViewModel>();
            
           /* Tabs = new List<TabDefinition>
            {
                new TabDefinition
                {
                    Element = Tab1,
                    Id = "ABOUT US",
                    Title = "ABOUT US",
                    IsSelected = true
                },
                new TabDefinition
                {
                    Element = Tab2,
                    Id = "DETAILS",
                    Title = "DETAILS",
                    IsSelected = false
                }
            };*/
        }

       /* public List<TabDefinition> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }*/

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            BindingContext.As<OwnerBusinessTabSettingsPageViewModel>().Initialize();
        }

        private void GoToBusinessAccountSettings(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSettingsBusinessAccountPage());
        }

        private void GoToBusinessCurrencySettings(object sender, HorizontalButtonEventArgs e)
        {
            Navigation.PushAsync(new OwnerBusinessTabSettingsBusinessCurrencyPage());
        }
    }
}