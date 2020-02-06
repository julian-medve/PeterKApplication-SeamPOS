using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Models;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.DpsTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DpsOffersTabPage : ContentPage
    {
        private List<TabDefinition> _tabs;
        private string _leftNavigationBarImage;

        public DpsOffersTabPage()
        {
            InitializeComponent();
            
            Tabs = new List<TabDefinition>
            {
                new TabDefinition
                {
                    Element = Tab1,
                    Id = "OFFERS",
                    Title = "OFFERS",
                    IsSelected = true
                },
                new TabDefinition
                {
                    Element = Tab2,
                    Id = "COUPONS",
                    Title = "COUPONS",
                    IsSelected = false
                }
            };
            
            if (AuthService.IsOwner)
            {
                LeftNavigationBarImage = "Menu.png";
            }
            else
            {
                LeftNavigationBarImage = "";
            }
        }
        
        public string LeftNavigationBarImage
        {
            get => _leftNavigationBarImage;
            set
            {
                _leftNavigationBarImage = value; 
                OnPropertyChanged(LeftNavigationBarImage);
            }
        }

        public List<TabDefinition> Tabs
        {
            get => _tabs;
            set {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }

        private void CouponList_ItemSelected(object sender, CouponListEventArgs e)
        {
        }

        private void CheckoutHoverMenu_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}