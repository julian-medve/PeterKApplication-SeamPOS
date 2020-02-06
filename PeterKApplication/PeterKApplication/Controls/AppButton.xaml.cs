using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PeterKApplication.Helpers;
using Xamarin.Forms;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// ALMOST all buttons except for image buttons are a combination of parameters of this element 
    /// </summary>
    public partial class AppButton : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
            typeof(AppButton), "BUTTON TEXT", BindingMode.TwoWay);

        public static readonly BindableProperty IsPrimaryProperty = BindableProperty.Create("IsPrimary", typeof(bool),
            typeof(AppButton), true, BindingMode.TwoWay, propertyChanged: ReevaluateParameters);

        public static readonly BindableProperty IsBigProperty = BindableProperty.Create("IsBig", typeof(bool),
            typeof(AppButton), true, BindingMode.TwoWay, propertyChanged: ReevaluateParameters);

        public static readonly BindableProperty IsOutlineProperty = BindableProperty.Create("IsOutline", typeof(bool),
            typeof(AppButton), false, BindingMode.TwoWay, propertyChanged: ReevaluateParameters);


        private static void ReevaluateParameters(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((AppButton) bindable).EvaluateThemeAndSize();
        }

        public void EvaluateThemeAndSize()
        {
            Console.WriteLine("Evaluating theme");

            if (IsPrimary)
            {
                BackgroundElement.BackgroundColor = StaticResourceHelper.Get<Color>("MainBlueColor");
                OutlineOverlay.BorderColor = StaticResourceHelper.Get<Color>("MainBlueColor");
            }
            else
            {
                BackgroundElement.BackgroundColor = StaticResourceHelper.Get<Color>("MainRedColor");
                OutlineOverlay.BorderColor = StaticResourceHelper.Get<Color>("MainRedColor");
            }

            var fontSize = 14;
            var gridPadding = 0;
            var gridHeight = 50;

            if (!IsBig)
            {
                fontSize = 10;
                gridPadding = 5;
                gridHeight = 30;
            }
            
            var buttonHeight = gridHeight;
            
            BackgroundElement.HeightRequest = buttonHeight;
            BackgroundElement.CornerRadius = buttonHeight/2;
            
            OutlineOverlay.HeightRequest = buttonHeight - 2;
            OutlineOverlay.CornerRadius = (buttonHeight - 2) / 2;
            OutlineOverlay.Margin = 1;
            
            TextElement.FontSize = fontSize;
            TextElement.Margin = new Thickness
            {
                Left = buttonHeight/2,
                Right = buttonHeight/2
            };
            
            Grid.Padding = gridPadding;
            Grid.HeightRequest = gridHeight;

            if (IsOutline)
            {
                if (IsPrimary)
                {
                    TextElement.TextColor = StaticResourceHelper.Get<Color>("MainBlueColor");
                }
                else
                {
                    TextElement.TextColor = StaticResourceHelper.Get<Color>("MainRedColor");
                }
            }
            else
            {
                TextElement.TextColor = Color.White;
            }
        }

        public event EventHandler OnClicked;
        
        public AppButton()
        {
            InitializeComponent();

            Grid.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { OnClicked?.Invoke(this, EventArgs.Empty); })
            });

            EvaluateThemeAndSize();
        }

        /// <summary>
        /// Button text, please use it
        /// </summary>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// If button is only outline, it has no background and button text becomes the same as button outline color, primary blue or secondary red depending on <paramref name="IsPrimary"/> parameter, default is false
        /// </summary>
        public bool IsOutline
        {
            get => (bool) GetValue(IsOutlineProperty);
            set => SetValue(IsOutlineProperty, value);
        }

        /// <summary>
        /// Determines the button intent, if primary button is blue otherwise red, default is true
        /// </summary>
        public bool IsPrimary
        {
            get => (bool) GetValue(IsPrimaryProperty);
            set => SetValue(IsPrimaryProperty, value);
        }

        /// <summary>
        /// If button is of bigger size, default is true
        /// </summary>
        public bool IsBig
        {
            get => (bool) GetValue(IsBigProperty);
            set => SetValue(IsBigProperty, value);
        }
    }
}