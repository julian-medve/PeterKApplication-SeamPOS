using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    public class TabControlEventArgs : EventArgs
    {
        public TabDefinition Tab { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabControl : ContentView
    {
        public static readonly BindableProperty TabsProperty = BindableProperty.Create("Tabs",
            typeof(List<TabDefinition>), typeof(TabControl), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty HasUnderlineProperty = BindableProperty.Create("HasUnderline",
            typeof(bool), typeof(TabControl), true, BindingMode.TwoWay, propertyChanged: Reevaluate);

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            bindable.As<TabControl>().Evaluate();
        }

        private void Evaluate()
        {
            Layout.Children.Clear();
            
            HeightRequest = Grid.HeightRequest = HasUnderline ? 42 : 72;
            
            var mainBlue = StaticResourceHelper.Get<Color>("MainBlueColor");

            Tabs?.ForEach(tab =>
            {
                if (tab.Element != null) tab.Element.IsVisible = tab.IsSelected;

                var rows = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = HasUnderline ? 20 : 30
                    },
                    new RowDefinition
                    {
                        Height = 2
                    }
                };

                var tg = new TapGestureRecognizer
                {
                    Command = new Command(() => SwitchToTab(tab))
                };

                var grid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions = rows
                };
                
                grid.GestureRecognizers.Add(tg);

                var lab = new Label
                {
                    Text = tab.Title,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = tab.IsSelected ? FontAttributes.Bold : FontAttributes.None,
                    TextColor = tab.IsSelected ? Color.Black : Color.Default
                };

                lab.GestureRecognizers.Add(tg);

                grid.Children.Add(lab, 0, 0);

                var bw = new BoxView
                {
                    BackgroundColor =
                        HasUnderline ? tab.IsSelected ? mainBlue : Color.Transparent : Color.Transparent,
                    HeightRequest = 2
                };
                
                bw.GestureRecognizers.Add(tg);

                grid.Children.Add(bw, 0, 1);

                Layout.Children.Add(grid);
            });
        }

        public void SwitchToTab(TabDefinition tab)
        {
            Console.WriteLine("Switching:" + tab.Id);
            Tabs = Tabs.Select(s =>
            {
                s.IsSelected = tab.Id == s.Id;
                return s;
            }).ToList();

            TabChanged?.Invoke(this, new TabControlEventArgs
            {
                Tab = tab
            });
        }

        public event EventHandler<TabControlEventArgs> TabChanged;

        public TabControl()
        {
            InitializeComponent();
        }

        public List<TabDefinition> Tabs
        {
            get => (List<TabDefinition>) GetValue(TabsProperty);
            set => SetValue(TabsProperty, value);
        }

        public bool HasUnderline
        {
            get => (bool) GetValue(HasUnderlineProperty);
            set => SetValue(HasUnderlineProperty, value);
        }
    }
}