using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfStrings : ContentView
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items", typeof(IEnumerable<string>),
            typeof(ListOfStrings), null, BindingMode.TwoWay, propertyChanged: ReevaluateChanges);

        private static void ReevaluateChanges(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ListOfStrings) bindable).Evaluate();
        }

        private void Evaluate()
        {
            StackLayout.Children.Clear();

            Items?.ForEach(i =>
            {
                StackLayout.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = i
                });
            });
        }

        public ListOfStrings()
        {
            InitializeComponent();
        }

        public IEnumerable<string> Items
        {
            get => (IEnumerable<string>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
    }
}