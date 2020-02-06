using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutCartList : ContentView
    {
        public static readonly BindableProperty ProductListProperty = BindableProperty.Create("ListItems",
            typeof(List<Product>), typeof(CheckoutCartList), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            bindable.As<CheckoutCartList>().Evaluate();
        }

        private void Evaluate()
        {
            if (ProductList?.Any() == true)
            {
                var lightGrayColor = StaticResourceHelper.Get<Color>("HeaderTopAndBottom");
                var mediumGrayColor = StaticResourceHelper.Get<Color>("MediumGrayColor");
                
                Layout.Children.Clear();

                ProductList.ForEach(product =>
                {
                    var outerStack = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 50,
                        Margin = new Thickness
                        {
                            Top = 0,
                            Bottom = 20,
                            Left = 20,
                            Right = 20
                        },
                    };
                    var topStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label
                            {
                                Text = product.Name,
                                TextColor = Color.Black,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                FontSize = 14
                            },
                            new Label
                            {
                                Text = AuthService.CurrentUser().CurrencyFormat + $"{Math.Round(product.Quantity * product.Price):N2}",
                                TextColor = Color.Black,
                                FontAttributes = FontAttributes.Bold,
                                FontSize = 16
                            }
                        }
                    };

                    outerStack.Children.Add(topStack);
                    outerStack.Children.Add(new Label
                    {
                        Text = $"Pcs: {product.Quantity} @ {Math.Round(product.Price):N2}",
                        TextColor = lightGrayColor,
                        FontSize = 12
                    });
                    outerStack.Children.Add(new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = mediumGrayColor,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    });

                    Layout.Children.Add(outerStack);
                });
            }
        }

        public CheckoutCartList()
        {
            InitializeComponent();
        }

        public List<Product> ProductList
        {
            get => (List<Product>) GetValue(ProductListProperty);
            set => SetValue(ProductListProperty, value);
        }
    }
}