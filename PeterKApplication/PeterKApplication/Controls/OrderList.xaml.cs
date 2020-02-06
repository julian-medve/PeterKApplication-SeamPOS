using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    public class OrderListDetailsEventArgs : EventArgs
    {
        public Order Order { get; set; }
    }
    
    /// <summary>
    /// Orders list, shows a list of grids where each has 6 cells
    /// 
    /// ORDER #123123----------details--
    /// status               o pending
    /// ship date            01 Apr 2015
    /// order amnt           $123.2
    /// ORDER #123123----------details--
    /// status               o pending
    /// ship date            01 Apr 2015
    /// order amnt           $123.2
    /// ORDER #123123----------details--
    /// 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderList : ContentView
    {
        public static readonly BindableProperty OrdersProperty = BindableProperty.Create("Orders", typeof(List<Order>), typeof(OrderList), new List<Order>(), BindingMode.TwoWay);

        public OrderList()
        {
            InitializeComponent();
            
            DetailsTapped = new Command<Order>(DetailsTappedExecuted);
        }

        /// <summary>
        /// When details text above and to the right of the list item is clicked
        /// </summary>
        public event EventHandler<OrderListDetailsEventArgs> DetailsClicked;

        private void DetailsTappedExecuted(Order obj)
        {
            DetailsClicked?.Invoke(this, new OrderListDetailsEventArgs
            {
                Order = obj
            });
        }

        /// <summary>
        /// List of orders to use, use it
        /// </summary>
        public List<Order> Orders
        {
            get => (List<Order>) GetValue(OrdersProperty);
            set => SetValue(OrdersProperty, value);
        }

        public ICommand DetailsTapped { get; }
    }
}