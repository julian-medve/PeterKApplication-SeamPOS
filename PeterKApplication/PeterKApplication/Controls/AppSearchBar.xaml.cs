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
    /// Search bar element, can have left and right side icon, changeable placeholder
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSearchBar : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(AppSearchBar), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty RightSideIconProperty = BindableProperty.Create("RightSideIcon", typeof(bool), typeof(AppSearchBar), true, BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(AppSearchBar), "Search", BindingMode.TwoWay);

        public AppSearchBar()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Search text content, please use it
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// If search icon is on the right, default is left
        /// </summary>
        public bool RightSideIcon
        {
            get => (bool)GetValue(RightSideIconProperty);
            set => SetValue(RightSideIconProperty, value);
        }

        /// <summary>
        /// Placeholder text, default is "Search", set it to empty if it really bothers you
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
}