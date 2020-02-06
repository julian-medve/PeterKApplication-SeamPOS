using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Use it
    /// </summary>
    public partial class Logo : ContentView
    {
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create("ImageHeight", typeof(int), typeof(Logo), 25, BindingMode.TwoWay);
        
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create("ImageWidth", typeof(int), typeof(Logo), null, BindingMode.TwoWay);

        public Logo()
        {
            InitializeComponent();
        }

        public int ImageHeight
        {
            get => (int) GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }

        public int ImageWidth
        {
            get => (int) GetValue(ImageWidthProperty);
            set => SetValue(ImageWidthProperty, value);
        }
    }
}
