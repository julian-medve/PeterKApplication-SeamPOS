using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Extensions;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerProductsTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerProductsTabAddProductPage : ContentPage
    {
        public OwnerProductsTabAddProductPage(ProductCategory bcSelectedCategory)
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerProductsTabAddProductPageViewModel>();

            var bc = BindingContext.As<OwnerProductsTabAddProductPageViewModel>();

            bc.SelectedCategoryName = bcSelectedCategory.Name;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var bc = BindingContext.As<OwnerProductsTabAddProductPageViewModel>();
            bc.Initialize();
        }

        private void CenterImageOrCirclePlus_OnImageChanged(object sender, CenterImageOrCirclePlusEventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabAddProductPageViewModel>();
            bc.SetImage(e.Image);
        }

        private async void AppButton_OnOnClicked(object sender, EventArgs e)
        {
            // save and new
            var bc = BindingContext.As<OwnerProductsTabAddProductPageViewModel>();
            await bc.SaveAndNew();
        }

        private async void AppButton_OnOnClicked2(object sender, EventArgs e)
        {
            var bc = BindingContext.As<OwnerProductsTabAddProductPageViewModel>();
            await bc.Save();
            await Navigation.PopAsync();
        }
    }
}