using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Vertical progress. Defined by progress steps, step can be "Done". Used in shipping, like this:
    /// ship date                Packaged by
    /// 01 Apr 2015      o       Staff
    ///                  |
    /// shipped date     |       Will be delivered
    /// 01 asdf asdf     O       by oct 12
    ///                  |
    ///                  |
    /// delivered        .       delivered to customer
    /// 
    /// 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerticalConnectedProgressRings : ContentView
    {
        public static readonly BindableProperty StepsProperty = BindableProperty.Create("Steps",
            typeof(List<VerticalConnectedProgressStep>), typeof(VerticalConnectedProgressRings),
            new List<VerticalConnectedProgressStep>(), propertyChanged: ReevaluateSteps);

        private static void ReevaluateSteps(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((VerticalConnectedProgressRings) bindable).Reevaluate();
        }

        private void Reevaluate()
        {
            StackLayout.Children.Clear();
            var gray = StaticResourceHelper.Get<Color>("MediumGrayColor");
            var blue = StaticResourceHelper.Get<Color>("MainBlueColor");
            var lastStepWasDone = false;
            for (var i = 0; i < Steps.Count; i++)
            {
                var step = Steps[i];
                var frame = new Frame
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BorderColor = lastStepWasDone ? blue : gray,
                    Margin = 0,
                    Padding = 0,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HasShadow = false,
                    CornerRadius = 10,
                    Content = new Frame
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        BorderColor = blue,
                        BackgroundColor = blue,
                        Margin = 0,
                        Padding = 0,
                        WidthRequest = step.IsDone ? 10 : 5,
                        HeightRequest = step.IsDone ? 10 : 5,
                        HasShadow = false,
                        CornerRadius = step.IsDone ? 7 : 3.5f
                    }
                };

                var frameLeft = new Frame
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = 0,
                    Padding = 0,
                    WidthRequest = 100,
                    HeightRequest = 100,
                    HasShadow = false,
                };

                var frameRight = new Frame
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = 0,
                    Padding = 0,
                    WidthRequest = 100,
                    HeightRequest = 100,
                    HasShadow = false,
                };

                StackLayout.Children.Add(frame);

                if (i < Steps.Count - 1)
                {
                    StackLayout.Children.Add(new BoxView
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Margin = -13,
                        BackgroundColor = step.IsDone ? blue : gray,
                        WidthRequest = step.IsDone ? 3 : 1
                    });
                }

                lastStepWasDone = step.IsDone;
            }
        }

        public VerticalConnectedProgressRings()
        {
            InitializeComponent();
            Reevaluate();
        }

        /// <summary>
        /// Progress steps, please keep in mind all done steps have to be one after the other and start from the start, k tnx
        /// </summary>
        public List<VerticalConnectedProgressStep> Steps
        {
            get => (List<VerticalConnectedProgressStep>) GetValue(StepsProperty);
            set => SetValue(StepsProperty, value);
        }
    }
}