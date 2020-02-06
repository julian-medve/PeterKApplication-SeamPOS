using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Models;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersOverviewData : ContentView
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create("Data", typeof(OrdersOverviewDataDefinition), typeof(OrdersOverviewData), null, BindingMode.TwoWay);

        public OrdersOverviewData()
        {
            InitializeComponent();
        }

      
        public OrdersOverviewDataDefinition Data
        {
            get => (OrdersOverviewDataDefinition)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public string CurrencyFormat => AuthService.CurrentUser().CurrencyFormat;
    }
}