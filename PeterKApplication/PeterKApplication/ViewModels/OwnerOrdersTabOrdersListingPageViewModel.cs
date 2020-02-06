using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;
/*using static Android.Resource;*/

namespace PeterKApplication.ViewModels
{
    public class OwnerOrdersTabOrdersListingPageViewModel : INotifyPropertyChanged
    {
        private string _search;
        private List<Order> _orders;
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerOrdersTabOrdersListingPageViewModel Self => null;

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                OnPropertyChanged(nameof(FilteredOrders));
            }
        }

        public List<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
                OnPropertyChanged(nameof(FilteredOrders));
            }
        }

        public List<Order> FilteredOrders => string.IsNullOrEmpty(Search)
            ? Orders
            : Orders?.Where(o => o.OrderNumber.ToString().Contains(Search) == true).ToList();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                Orders = db.Orders
                    .Include(o => o.OrderItems)
                    .Include(o => o.PaymentType)
                    .OrderByDescending(o => o.CreatedOn).ToList();

                List<Order> tempOrders = new List<Order>();

                string currentUserId = AuthService.CurrentUser().Id;

                foreach (Order item in Orders)
                    if (item.AppUserId.Equals(currentUserId))
                        tempOrders.Add(item);

                Orders = tempOrders;
            
                string currencyFormat = AuthService.CurrentUser().CurrencyFormat;

                for (int i = 0; i < Orders.Count; i++)
                {
                    Orders[i].AmountLabel   = currencyFormat + Orders[i].Amount;   
                    Orders[i].DiscountLabel = currencyFormat + Orders[i].Discount;
                    Orders[i].DeliveryPriceLabel = currencyFormat + Orders[i].DeliveryPrice;

                    Orders[i].OrderProductItems = new List<Product>();

                    foreach (OrderItem orderItem in Orders[i].OrderItems) {

                        Product product = db.Products.First(f=> f.Id.Equals(orderItem.ProductId));

                        if (product != null) {
                            product.Quantity = orderItem.Quantity;
                            Orders[i].OrderProductItems.Add(product);
                        }
                    }

                    Orders[i].OrderProductsHeight = Orders[i].OrderProductItems.Count * 90;
                }
            }
        }
    }
}