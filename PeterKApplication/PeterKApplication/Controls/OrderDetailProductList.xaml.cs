using PeterKApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailProductList : ContentView
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create("Products", typeof(List<Product>), typeof(OrderDetailProductList), null, BindingMode.TwoWay);

        public static readonly BindableProperty DefaultImageProperty = BindableProperty.Create("DefaultImage", typeof(string), typeof(OrderDetailProductList), null, BindingMode.TwoWay);

        public static readonly BindableProperty SelectedImageProperty = BindableProperty.Create("SelectedImage", typeof(string), typeof(OrderDetailProductList), null, BindingMode.TwoWay);


        public OrderDetailProductList()
        {
            InitializeComponent();
        }


        public List<Product> Products
        {
            get => (List<Product>)GetValue(ProductsProperty);
            set => SetValue(ProductsProperty, value);
        }

        public string DefaultImage
        {
            get => (string)GetValue(DefaultImageProperty);
            set => SetValue(DefaultImageProperty, value);
        }

        public string SelectedImage
        {
            get => (string)GetValue(SelectedImageProperty);
            set => SetValue(SelectedImageProperty, value);
        }

        public class ProductListEventArgs : EventArgs
        {
            public Product Product { get; set; }
        }
    }
}