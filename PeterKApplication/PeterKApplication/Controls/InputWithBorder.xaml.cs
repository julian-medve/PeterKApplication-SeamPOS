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
    /// Main input with border
    /// [input field]
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputWithBorder : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
            typeof(InputWithBorder), null, BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder",
            typeof(string),
            typeof(InputWithBorder), null, BindingMode.TwoWay);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard",
                    typeof(Keyboard),
                    typeof(InputWithBorder), null, BindingMode.TwoWay);

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword",
                   typeof(bool),
                   typeof(InputWithBorder), null, BindingMode.TwoWay);


        private Color _borderColor = StaticResourceHelper.Get<Color>("MediumGrayColor");

        public InputWithBorder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form text, please use it
        /// </summary>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Form placeholder
        /// </summary>
        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                OnPropertyChanged(nameof(BorderColor));
            }
        }

        public Keyboard Keyboard 
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }


        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            BorderColor = StaticResourceHelper.Get<Color>("MainBlueColor");
        }

        private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
        {
            BorderColor = StaticResourceHelper.Get<Color>("MediumGrayColor");
        }
    }
}