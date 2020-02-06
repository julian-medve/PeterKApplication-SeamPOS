using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Used in owner dashboard, shows total saless, KES and big 0, last 7 days, last 30 days, kes 0k and kas 0k, very useful
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewData : ContentView
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create("Data", typeof(List<OverviewDataCenterDefinition>), typeof(OverviewData), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty SummaryProperty = BindableProperty.Create("Summary", typeof(OverviewDataSummaryDefinition), typeof(OverviewData), null, BindingMode.TwoWay);

        
        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            bindable.As<OverviewData>().Evaluate();
        }

        private void Evaluate()
        {
            MainData.Children.Clear();

            if (Data?.Any() == true)
            {
                Data.ForEach(datum =>
                {
                    MainData.Children.Add(new OverviewDataCenter
                    {
                        Data = datum
                    });
                });
            }
        }

        public OverviewData()
        {
            InitializeComponent();
        }

        public List<OverviewDataCenterDefinition> Data
        {
            get => (List<OverviewDataCenterDefinition>)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public OverviewDataSummaryDefinition Summary
        {
            get => (OverviewDataSummaryDefinition) GetValue(SummaryProperty);
            set => SetValue(SummaryProperty, value);
        }

        public string CurrencyFormat => AuthService.CurrentUser().CurrencyFormat;
    }
}