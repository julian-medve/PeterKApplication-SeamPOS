using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// A number with circle background, used in DPS for amount and some walk-through pages, can be red or blue
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircledNumber : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CircledNumber), "0", BindingMode.TwoWay);
        public static readonly BindableProperty IsPrimaryProperty = BindableProperty.Create("IsPrimary", typeof(bool),
            typeof(CircledNumber), true, BindingMode.TwoWay, propertyChanged: ReevaluateParameters);

        public event EventHandler Clicked;

        private static void ReevaluateParameters(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((CircledNumber) bindable).EvaluateThemeAndSize();
        }

        private void EvaluateThemeAndSize()
        {
            if (IsPrimary)
            {
                BackgroundElement.BackgroundColor = StaticResourceHelper.Get<Color>("MainBlueColor");
            }
            else
            {
                BackgroundElement.BackgroundColor = StaticResourceHelper.Get<Color>("MainRedColor");
            }
        }

        public CircledNumber()
        {
            InitializeComponent();
            EvaluateThemeAndSize();
        }

        /// <summary>
        /// Text to show
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// If primary then blue otherwise red, default is true
        /// </summary>
        public bool IsPrimary
        {
            get => (bool) GetValue(IsPrimaryProperty);
            set => SetValue(IsPrimaryProperty, value);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
        }
    }
}