using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using PeterKApplication.Extensions;

namespace PeterKApplication.Pages
{
    public partial class ReferencePopup : PopupPage
    {
        private string _reference;
        CheckoutPage parentCheckoutPage;


        public ReferencePopup(CheckoutPage parentCheckout)
        {
            InitializeComponent();
            parentCheckoutPage = parentCheckout;
        }

        public string ReferencePayment
        {
            get => _reference;
            set
            {
                _reference = value;
                OnPropertyChanged(nameof(ReferencePayment));
            }
        }

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                return;
            }

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if(!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            await Task.WhenAll(
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if(!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;


            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private async void SaveAndContinue(object sender, EventArgs e)
        {
            parentCheckoutPage.SetReferenceValue(_reference);
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
