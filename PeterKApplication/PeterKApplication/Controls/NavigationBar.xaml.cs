using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : ContentView
    {
        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create("HasBackButton",
            typeof(bool), typeof(NavigationBar), false, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
            typeof(NavigationBar), null, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty RightImageProperty = BindableProperty.Create("RightImage",
            typeof(string), typeof(NavigationBar), null, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty LeftImageProperty = BindableProperty.Create("LeftImage",
            typeof(string), typeof(NavigationBar), null, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty CenterImageProperty = BindableProperty.Create("CenterImage",
            typeof(string), typeof(NavigationBar), null, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty CenterImageHeightProperty = BindableProperty.Create("CenterImageHeight",
            typeof(int), typeof(NavigationBar), 24, BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty LeftImageTappedCommandProperty =
            BindableProperty.Create("LeftImageTappedCommand", typeof(ICommand), typeof(NavigationBar), null,
                BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty RightImageTappedCommandProperty =
            BindableProperty.Create("RightImageTappedCommand", typeof(ICommand), typeof(NavigationBar), null,
                BindingMode.TwoWay, propertyChanged:Reevaluate);

        public static readonly BindableProperty CenterImageTappedCommandProperty =
            BindableProperty.Create("CenterImageTappedCommand", typeof(ICommand), typeof(NavigationBar), null,
                BindingMode.TwoWay, propertyChanged:Reevaluate);

        private int _rowHeight;

        public NavigationBar()
        {
            InitializeComponent();
        }

        public bool HasBackButton
        {
            get => (bool)GetValue(HasBackButtonProperty);
            set => SetValue(HasBackButtonProperty, value);
        }

        /// <summary>
        /// Text, use it
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string RightImage
        {
            get => (string)GetValue(RightImageProperty);
            set => SetValue(RightImageProperty, value);
        }

        public string LeftImage
        {
            get => (string)GetValue(LeftImageProperty);
            set => SetValue(LeftImageProperty, value);
        }

        public string CenterImage
        {
            get => (string)GetValue(CenterImageProperty);
            set => SetValue(CenterImageProperty, value);
        }

        public int CenterImageHeight
        {
            get => (int)GetValue(CenterImageHeightProperty);
            set => SetValue(CenterImageHeightProperty, value);
        }

        public int RowHeight
        {
            get => _rowHeight;
            set
            {
                _rowHeight = value;
                OnPropertyChanged(nameof(RowHeight));
            }
        }

        public ICommand LeftImageTappedCommand
        {
            get => (ICommand)GetValue(LeftImageTappedCommandProperty);
            set => SetValue(LeftImageTappedCommandProperty, value);
        }

        public ICommand RightImageTappedCommand
        {
            get => (ICommand)GetValue(RightImageTappedCommandProperty);
            set => SetValue(RightImageTappedCommandProperty, value);
        }

        public ICommand CenterImageTappedCommand
        {
            get => (ICommand)GetValue(CenterImageTappedCommandProperty);
            set => SetValue(CenterImageTappedCommandProperty, value);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopAsync();
            }
            catch
            {
            }
        }

        public event EventHandler RightImageTapped;
        public event EventHandler LeftImageTapped;
        public event EventHandler CenterImageTapped;

        private void TapGestureRecognizer_OnTapped2(object sender, EventArgs e)
        {
            RightImageTapped?.Invoke(this, null);
        }

        private void TapGestureRecognizer_OnTapped3(object sender, EventArgs e)
        {
            LeftImageTapped?.Invoke(this, null);
            LeftImageTappedCommand?.Execute(null);
        }

        private void TapGestureRecognizer_OnTapped4(object sender, EventArgs e)
        {
            CenterImageTapped?.Invoke(this, null);
            CenterImageTappedCommand.Execute(null);
        }

        private static void Reevaluate(BindableObject bindable, object oldValue, object newValue)
        {
            ((NavigationBar)bindable).Evaluate();
        }

        private void Evaluate()
        {
            RowHeight = CenterImageHeight + 6;
        }

    }
}