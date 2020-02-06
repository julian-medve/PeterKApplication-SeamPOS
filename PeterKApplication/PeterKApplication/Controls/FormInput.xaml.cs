using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Input with a an optional label to the left
    /// LABEL [input field]
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormInput : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
            typeof(FormInput), null, BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder",
            typeof(string),
            typeof(FormInput), null, BindingMode.TwoWay);

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string),
            typeof(FormInput), null, BindingMode.TwoWay);

        public static readonly BindableProperty ShowChangeButtonProperty =
            BindableProperty.Create("ShowChangeButton", typeof(bool), typeof(FormInput), false, BindingMode.TwoWay,
                propertyChanged: Reevaluate);

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((FormInput)bindable).Evaluate();
        }

        private bool _enableEntry = true;

        public FormInput()
        {
            InitializeComponent();
            Evaluate();
        }

        private void Evaluate()
        {
            if (ShowChangeButton)
            {
                EnableEntry = false;
            }
        }

        /// <summary>
        /// Form text, please use it
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Form placeholder
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Form label
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        /// Should show change button on the right?
        /// </summary>
        public bool ShowChangeButton
        {
            get => (bool)GetValue(ShowChangeButtonProperty);
            set => SetValue(ShowChangeButtonProperty, value);
        }

        public bool EnableEntry
        {
            get => _enableEntry;
            set
            {
                _enableEntry = value;
                OnPropertyChanged(nameof(EnableEntry));
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            EnableEntry = true;
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {

        }

        private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
        {

        }
    }
}