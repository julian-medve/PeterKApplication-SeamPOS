using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages.OwnerProductsTabPages;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerProductsTabPage : ContentPage
    {
        private List<TabDefinition> _tabs;
        private bool _showDropdownMenu;

        public OwnerProductsTabPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Resolve<OwnerProductsTabPageViewModel>();

            MessagingCenter.Subscribe<OwnerTabbedPage, OwnerTabPageSubCategory>(this, "GoToSubCategory",
                (page, category) =>
                {
                    switch (category)
                    {
                        case OwnerTabPageSubCategory.Offers:
                            break;
                    }
                });
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

        public bool ShowDropdownMenu
        {
            get => _showDropdownMenu;
            set
            {
                _showDropdownMenu = value;
                OnPropertyChanged(nameof(ShowDropdownMenu));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            bc.Initialize();
            CreateCategoryTabs();
        }

        private void CreateCategoryTabs()
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

            List <TabDefinition> tempTabs = bc.Categories.Select((s, i) => new TabDefinition
            {
                Object = s,
                Title = s.Name,
                IsSelected = false,
                Id = s.Id.ToString()
            }).ToList();

            // Add "All" into the category list
            ProductCategory category = new ProductCategory();
            category.Name = "All";

            tempTabs.Insert(0, new TabDefinition { Object = category, Title = category.Name, IsSelected = true, Id = category.Id.ToString()}) ;

            Tabs = tempTabs;
        }

        public void ProductItemTapped(object sender, ProductListEventArgs args)
        {
            // Not calling, looks strange.
            BindingContext.As<OwnerProductsTabPageViewModel>().ItemTapped(args.Product);
        }

        private void TabControl_OnTabChanged(object sender, TabControlEventArgs e)
        {
            BindingContext.As<OwnerProductsTabPageViewModel>().ChangeCategory(e.Tab);
        }

        private void NavigationBar_OnRightImageTapped(object sender, EventArgs e)
        {
            ShowDropdownMenu = !ShowDropdownMenu;
        }

        private void CheckoutHoverMenu_OnClicked(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

            Navigation.PushAsync(new CheckoutPage(bc.Products));
        }

        private void Tab1_AddOneClicked(object sender, ProductListEventArgs args)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            bc.AddOwnerProductQuantity(args.Product, 1);
        }

        private void Tab1_DeleteAllClicked(object sender, ProductListEventArgs args)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();

            bc.RemoveAllOwnerProductsQuantityAsync(args.Product);
        }


        private async void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            // add category
            var res = await UserDialogs.Instance.PromptAsync("Enter category name", "Add category", "Save", "Cancel", "Category name");
            if (res.Ok)
            {
                await bc.AddCategory(res.Text);
                CreateCategoryTabs();
            }
        }

        private void TapGestureRecognizer_OnTapped2(object sender, EventArgs e)
        {
            // cart mode
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            bc.SetMode(bc.IsCartMode ? ProductPageModes.AddProduct : ProductPageModes.CartMode);
        }

        private void TapGestureRecognizer_OnTapped3(object sender, EventArgs e)
        {
            // share mode
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            bc.SetMode(bc.IsShareMode ? ProductPageModes.AddProduct : ProductPageModes.ShareMode);
        }

        private void TapGestureRecognizer_OnTapped4(object sender, EventArgs e)
        {
            // offers
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            bc.SetMode(bc.IsOffersMode ? ProductPageModes.AddProduct : ProductPageModes.OffersMode);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabPageViewModel>();
            if (bc.HasAnyCategory)
            {
                Navigation.PushAsync(new OwnerProductsTabAddProductPage(bc.SelectedCategory));
            }
        }
    }
}