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
    public partial class OverviewDataCenter : ContentView
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create("Data", typeof(OverviewDataCenterDefinition), typeof(OverviewDataCenter), null, BindingMode.TwoWay);

        public string CurrencyFormat => AuthService.CurrentUser().CurrencyFormat;

        public OverviewDataCenter()
        {
            InitializeComponent();
        }

        public OverviewDataCenterDefinition Data
        {
            get => (OverviewDataCenterDefinition)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}