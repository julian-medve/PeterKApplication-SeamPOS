using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Extensions;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FFImageLoading.Forms;
using System.Threading;
using Rg.Plugins.Popup.Services;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        private bool _isCartFinish;
        private bool _isPaymentFinish;
        private string _headerTitle;
        private string _reference;

        private ReferencePopup referencePopup;

        Thread syncGifThread;

        public CheckoutPage(List<Product> productList)
        {
            InitializeComponent();

            referencePopup = new ReferencePopup();

            HeaderTitle = "Cart";

            try
            {
                BindingContext = DependencyService.Resolve<CheckoutViewModel>();
                BindingContext.As<CheckoutViewModel>().Products = productList.Where(w => w.IsSelected).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write("Exception : " + e.ToString());    
            }
        }

        public void SetReferenceValue(string value)
        {
            _reference = value;
            ShowConfirmationPage();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CartStage.IsVisible = false;
            PaymentStage.IsVisible = true;
            ConfirmationStage.IsVisible = false;
            IsCartFinish = true;
            HeaderTitle = "Select Payment Type";
            
            // process payment
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            // pending, save order for later
            await BindingContext.As<CheckoutViewModel>().Save(OrderStatus.Pending);
            await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            // canceled, save order
            await BindingContext.As<CheckoutViewModel>().Save(OrderStatus.Cancelled);
            await Navigation.PopAsync();
        }

        private async void SendAsInvoice(object sender, EventArgs e) {

            string filename = "Invoice_" + CurrentDateTime();
            await BindingContext.As<CheckoutViewModel>().SaveCartAsInvoiceAsync(filename);
        }

        public bool IsCartFinish
        {
            get => _isCartFinish;
            set
            {
                _isCartFinish = value;
                OnPropertyChanged(nameof(IsCartFinish));
            }
        }

        public bool IsPaymentFinish
        {
            get => _isPaymentFinish;
            set
            {
                _isPaymentFinish = value;
                OnPropertyChanged(nameof(IsPaymentFinish));
            }
        }

        public string HeaderTitle
        {
            get => _headerTitle;
            set
            {
                _headerTitle = value;
                OnPropertyChanged(nameof(HeaderTitle));
            }
        }

        private async void NavigationBar_LeftImageTapped(object sender, EventArgs e)
        {
            if (IsPaymentFinish)
            {
                CartStage.IsVisible = false;
                PaymentStage.IsVisible = true;
                ConfirmationStage.IsVisible = false;
                IsCartFinish = true;
                IsPaymentFinish = false;
                HeaderTitle = "Select Payment Type";
            }
            else if (IsCartFinish)
            {
                CartStage.IsVisible = true;
                PaymentStage.IsVisible = false;
                ConfirmationStage.IsVisible = false;
                IsCartFinish = false;
                HeaderTitle = "Cart";
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            BindingContext.As<CheckoutViewModel>().InitializeAsync();
        }

        private async void PairedList_ListItemTapped(object sender, Controls.PairedListEventArgs e)
        {
            if (e.Item.Name.Contains("Cheque") || e.Item.Name.Contains("Bank"))
                await PopupNavigation.Instance.PushAsync(referencePopup);
            else
                ShowConfirmationPage();
        }

        private async void ShowConfirmationPage()
        {

            CartStage.IsVisible = false;
            PaymentStage.IsVisible = false;
            ConfirmationStage.IsVisible = true;
            IsPaymentFinish = true;
            HeaderTitle = "Finish";

            await BindingContext.As<CheckoutViewModel>().Save(OrderStatus.Paid, _reference);

            syncGifThread = new Thread(async () => await SyncGifImage());
            syncGifThread.Start();
        }

        private async Task SyncGifImage()
        {
            while (true)
            {
                if (ConfirmationStage.IsVisible)
                {
                    BindingContext.As<CheckoutViewModel>().CheckoutImage = "checkout_progress.gif";
                    Thread.Sleep(800);
                    BindingContext.As<CheckoutViewModel>().CheckoutImage = "checkout_completed.png";

                    syncGifThread.Abort();
                }
            }
        }

        private async void AppButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void PrintReceipt(object sender, EventArgs e)
        {
            string filename = "PrintReceipt_" + CurrentDateTime();
            await BindingContext.As<CheckoutViewModel>().PrintReceipt();
        }

        private async void EmailReceipt(object sender, EventArgs e)
        {
            string filename = "CustomerPDF_" + CurrentDateTime();
            await BindingContext.As<CheckoutViewModel>().SaveCartAsInvoiceAsync(filename);
        }

        private async void TextReceipt(object sender, EventArgs e)
        {
            string filename = "CustomerPDF_" + CurrentDateTime();
            await BindingContext.As<CheckoutViewModel>().ShareReceipt(filename);
        }

        private string CurrentDateTime() {
            return DateTime.Now.ToString("yyyyMMdd_Hmmss");
        }
    }
}