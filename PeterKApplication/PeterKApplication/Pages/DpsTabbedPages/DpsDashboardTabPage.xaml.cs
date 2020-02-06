using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.DpsTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DpsDashboardTabPage : ContentPage
    {

        private List<TabDefinition> _tabs;
        private int _amount;
        private int _selectedAmount;
        private bool _canSelect;

        public DpsDashboardTabPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Resolve<OwnerProductsTabPageViewModel>();

            _canSelect = false;

            Tabs = new List<TabDefinition>
            {
                new TabDefinition
                {
                    Title = "TOP SELLERS",
                    Element = Tab1,
                    IsSelected = true,
                    Id = "TOP SELLERS"
                },
                new TabDefinition
                {
                    Title = "CATEGORIES",
                    Element = Tab2,
                    Id = "CATEGORIES"
                },
                new TabDefinition
                {
                    Title = "MPRODUCTS",
                    Element = Tab3,
                    Id = "MPRODUCTS"
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext.As<OwnerProductsTabPageViewModel>().Initialize();
        }

        private void PairedList_OnListItemTapped(object sender, PairedListEventArgs args)
        {
            BindingContext.As<OwnerProductsTabPageViewModel>().CategoryTapped(args.Item.Id.ToString());
            tabctrl.SwitchToTab(Tabs.First());
        }

        private void ProductList_ItemSelected(object sender, ProductListEventArgs args)
        {
            if (!App.ContextMenuShowing)
            {
                var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

                bc.TapOperation(args.Product);
            }
        }

        private void MeasuredProductList_ItemSelected(object sender, ProductListEventArgs args)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            if (_canSelect)
            {
                var q = _selectedAmount / args.Product.Price;
                bc.AddQuantity(args.Product, q);
                _canSelect = false;
                _selectedAmount = 0;
            }
        }

        public List<TabDefinition> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _canSelect = true;
            _selectedAmount = _amount;
        }

        private void Tab1_AddOneClicked(object sender, ProductListEventArgs args)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

            bc.AddQuantity(args.Product, 1);
        }

        private void Tab1_DeleteAllClicked(object sender, ProductListEventArgs args)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

            bc.RemoveAllQuantity(args.Product);
        }

        private void CheckoutHoverMenu_OnClicked(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            
            Navigation.PushAsync(new CheckoutPage(bc.Products));
        }
    }
}