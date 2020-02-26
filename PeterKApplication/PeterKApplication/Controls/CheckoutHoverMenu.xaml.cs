using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutHoverMenu : ContentView
    {
        public static readonly BindableProperty NumberProperty = BindableProperty.Create("Number", typeof(int), typeof(CheckoutHoverMenu), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty TextProperty  = BindableProperty.Create("Text", typeof(string), typeof(CheckoutHoverMenu), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty PrimaryProperty  = BindableProperty.Create("Primary", typeof(bool), typeof(CheckoutHoverMenu), true, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty DeleteAllClickedCommandProperty =
            BindableProperty.Create("DeleteAllClickedCommand", typeof(ICommand), typeof(ProductList), null,
                BindingMode.TwoWay);

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            bindable.As<CheckoutHoverMenu>().Evaluate();
        }

        private void Evaluate()
        {
            if (Primary)
            {
                StackLayout.BackgroundColor = StaticResourceHelper.Get<Color>("CheckoutMenuMainBlue");
            }else{
                StackLayout.BackgroundColor = StaticResourceHelper.Get<Color>("CheckoutMenuMainRed");
            }
        }
        
        

        public CheckoutHoverMenu()
        {
            InitializeComponent();
            
            Evaluate();
            
            StackLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this, null);
                })
            });

                DeleteAllCommand = new Command( () =>
                {
                    DeleteAllClicked?.Invoke(this, null);
                });
        }

        public int Number
        {
            get => (int) GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public bool Primary
        {
            get => (bool) GetValue(PrimaryProperty);
            set => SetValue(PrimaryProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public event EventHandler Clicked;

        public event EventHandler DeleteAllClicked;
        public Command DeleteAllCommand { get; set; }

    }
}