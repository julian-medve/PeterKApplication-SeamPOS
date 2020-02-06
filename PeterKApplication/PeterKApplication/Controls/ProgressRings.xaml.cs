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
    /// Shows a horizontal array of gray rings. Takes rings number and progress number as parameters. Rings are gray by default and blue if progress is less than the index of current ring.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressRings : ContentView
    {
        public static readonly BindableProperty RingsProperty = BindableProperty.Create("Rings", typeof(int),
            typeof(AppButton), 5, BindingMode.TwoWay, propertyChanged: ReevaluateRings);

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create("Progress", typeof(int),
            typeof(AppButton), 1, BindingMode.TwoWay, propertyChanged: ReevaluateRings);


        private static void ReevaluateRings(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ProgressRings) bindable).Reevaluate();
        }

        public ProgressRings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Number of rings to render, default 5
        /// </summary>
        public int Rings
        {
            get => (int) GetValue(RingsProperty);
            set => SetValue(RingsProperty, value);
        }

        /// <summary>
        /// How far along have the rings progressed? default 1
        /// </summary>
        public int Progress
        {
            get => (int) GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        private void Reevaluate()
        {
            var mainBlue = StaticResourceHelper.Get<Color>("MainBlueColor");
            var gray = StaticResourceHelper.Get<Color>("LightDarkGrayColor");
            StackLayout.Children.Clear();
            for (var i = 0; i < Rings; i++)
            {
                StackLayout.Children.Add(new Grid
                {
                    WidthRequest = 20,
                    HeightRequest = 20,
                    Children =
                    {
                        new Frame
                        {
                            WidthRequest = 20,
                            HeightRequest = 20,
                            CornerRadius = 10,
                            BorderColor = mainBlue,
                            HasShadow = false,
                            IsVisible = i < Progress
                        },
                        new BoxView
                        {
                            WidthRequest = 10,
                            HeightRequest = 10,
                            CornerRadius = 5,
                            Margin = 5,
                            BackgroundColor = i < Progress ? mainBlue : gray
                        }
                    }
                });
            }
        }
    }
}