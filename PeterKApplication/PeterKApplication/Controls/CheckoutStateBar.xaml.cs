using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutStateBar : ContentView
    {
        public static readonly BindableProperty IsCartFinishProperty = BindableProperty.Create("IsCartFinish",
            typeof(bool), typeof(CheckoutStateBar), false, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty IsPaymentFinishProperty = BindableProperty.Create("IsPaymentFinish",
            typeof(bool), typeof(CheckoutStateBar), false, BindingMode.TwoWay, propertyChanged:Reevaluate);

        private static void Reevaluate(BindableObject bindable, object oldValue, object newValue)
        {
            ((CheckoutStateBar)bindable).Evaluate();
        }

        private void Evaluate()
        {
            if (IsPaymentFinish)
            {
                ProgressBar1.Source = "progress_bar_blue.png";
                ProgressBar2.Source = "progress_bar_blue.png";
                Payment.Source = "checkout_list.png";
                Confirmation.Source = "checkout_list.png";
            }
            else if (IsCartFinish)
            {
                ProgressBar1.Source = "progress_bar_blue.png";
                ProgressBar2.Source = "progress_bar.png";
                Payment.Source = "payment_blue.png";
                Confirmation.Source = "confirm_payment.png";
            }
            else
            {
                ProgressBar1.Source = "progress_bar.png";
                ProgressBar2.Source = "progress_bar.png";
                Payment.Source = "payment.png";
                Confirmation.Source = "confirm_payment.png";
            }
        }

        public CheckoutStateBar()
        {
            InitializeComponent();
        }

        public bool IsCartFinish
        {
            get => (bool)GetValue(IsCartFinishProperty);
            set => SetValue(IsCartFinishProperty, value);
        }

        public bool IsPaymentFinish
        {
            get => (bool)GetValue(IsPaymentFinishProperty);
            set => SetValue(IsPaymentFinishProperty, value);
        }

    }
}