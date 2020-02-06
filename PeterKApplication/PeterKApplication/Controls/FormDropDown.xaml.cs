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
    /// Dropdown input field, with optional left label
    /// Label [dropdown ^]
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormDropDown : ContentView
    {
        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string),
            typeof(FormDropDown), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty OptionsProperty = BindableProperty.Create("Options",
            typeof(List<string>), typeof(FormDropDown), new List<string>(), BindingMode.TwoWay);

        public static readonly BindableProperty SelectedOptionProperty = BindableProperty.Create("SelectedOption",
            typeof(string), typeof(FormDropDown), null, BindingMode.TwoWay);

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(FormDropDown), null, BindingMode.TwoWay);

        public FormDropDown()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Label, not required
        /// </summary>
        public string Label
        {
            get => (string) GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        /// Selected option
        /// </summary>
        public string SelectedOption
        {
            get => (string) GetValue(SelectedOptionProperty);
            set => SetValue(SelectedOptionProperty, value);
        }

        public List<string> Options
        {
            get => (List<string>) GetValue(OptionsProperty);
            set => SetValue(OptionsProperty, value);
        }

        public string Title
        {
            get => (string) GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => Picker.Focus());
        }
    }
}