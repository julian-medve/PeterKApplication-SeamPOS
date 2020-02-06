using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Specific order details
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetails : ContentView
    {
        public static readonly BindableProperty OrderProperty = BindableProperty.Create("Order", typeof(Order), typeof(OrderDetails), null, BindingMode.TwoWay);
        public OrderDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Order with which to fill the data, use it
        /// </summary>
        public Order Order
        {
            get => (Order) GetValue(OrderProperty);
            set => SetValue(OrderProperty, value);
        }
    }
}