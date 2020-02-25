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
    /// Called when an item is clicked
    /// </summary>
    public class PairedListEventArgs : EventArgs
    {
        /// <summary>
        /// Clicked list item
        /// </summary>
        public PairedListItem Item { get; set; }
    }

    /// <summary>
    /// Shows items with a picture and a name in a paired list
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PairedList : ContentView
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items",
            typeof(List<PairedListPair>), typeof(PairedList),
            new List<PairedListPair>(), BindingMode.TwoWay);

        public static readonly BindableProperty SingleSelectionProperty =
            BindableProperty.Create("SingleSelection", typeof(bool), typeof(PairedList), false, BindingMode.TwoWay);

        public PairedList()
        {
            InitializeComponent();

            ItemTapped = new Command<PairedListItem>(ListItemItemTapped);
        }

        /// <summary>
        /// List item clicked handler
        /// </summary>
        public event EventHandler<PairedListEventArgs> ListItemTapped;

        private void ListItemItemTapped(PairedListItem obj)
        {
            Items = new List<PairedListPair>(Items.Select(s =>
            {
                if (s.Item1 != null && s.Item1.Id == obj.Id)
                {
                    s.Item1.Selected = !s.Item1.Selected;
                    if(s.Item2 != null && SingleSelection) s.Item2.Selected = false;
                }
                else if (s.Item2 != null && s.Item2.Id == obj.Id)
                {
                    if(s.Item1 != null && SingleSelection) s.Item1.Selected = false;
                    s.Item2.Selected = !s.Item2.Selected;
                }
                else
                {
                    if(s.Item1 != null && SingleSelection) s.Item1.Selected = false;
                    if(s.Item2 != null && SingleSelection) s.Item2.Selected = false;
                }

                if (s.Item1 != null) 
                {
                    if (s.Item1.Color == StaticResourceHelper.Get<Color>("MediumGrayColor"))
                        Console.WriteLine(string.Format("{0} : {1}", s.Item1.Name, "MediumGrayColor"));

                    if (s.Item1.Color == StaticResourceHelper.Get<Color>("LightGrayColor"))
                        Console.WriteLine(string.Format("{0} : {1}", s.Item1.Name, "LightGrayColor"));

                }

                if (s.Item2 != null)
                {
                    if (s.Item2.Color == StaticResourceHelper.Get<Color>("MediumGrayColor"))
                        Console.WriteLine(string.Format("{0} : {1}", s.Item2.Name, "MediumGrayColor"));

                    if (s.Item2.Color == StaticResourceHelper.Get<Color>("LightGrayColor"))
                        Console.WriteLine(string.Format("{0} : {1}", s.Item2.Name, "LightGrayColor"));

                }

                return s;
            }));

            ListItemTapped?.Invoke(this, new PairedListEventArgs
            {
                Item = obj
            });
        }

        /// <summary>
        /// List items, use it
        /// </summary>
        public IEnumerable<PairedListPair> Items
        {
            get => (IEnumerable<PairedListPair>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        /// <summary>
        /// Should only one item be selectable at a time? default false
        /// </summary>
        public bool SingleSelection
        {
            get => (bool) GetValue(SingleSelectionProperty);
            set => SetValue(SingleSelectionProperty, value);
        }

        public Command<PairedListItem> ItemTapped { get; }
    }
}