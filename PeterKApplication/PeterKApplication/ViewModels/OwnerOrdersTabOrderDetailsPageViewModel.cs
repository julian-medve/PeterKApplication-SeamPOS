using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeterKApplication.Annotations;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerOrdersTabOrderDetailsPageViewModel : INotifyPropertyChanged
    {
        private Order _order;
        private List<VerticalConnectedProgressStep> _orderSteps;
        public event PropertyChangedEventHandler PropertyChanged;

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
                OnPropertyChanged(nameof(OrderSteps));
            }
        }

        public static OwnerOrdersTabOrderDetailsPageViewModel Self => null;

        public List<VerticalConnectedProgressStep> OrderSteps => new List<VerticalConnectedProgressStep>
        {
            new VerticalConnectedProgressStep
            {
                Title = "Order Prepared By",
                Subtitle = Order?.AppUser?.FirstName,
                IsDone = true,
                LeftText = DateTime.Now.ToString("d")
            },
            
            new VerticalConnectedProgressStep
            {
                Title = "Status",
                Subtitle = Order?.OrderStatus.ToString(),
                IsDone = Order?.OrderStatus == OrderStatus.Paid,
                LeftText = DateTime.Now.ToString("d")
            },
            
            new VerticalConnectedProgressStep
            {
                Title = "Location",
                Subtitle = Order?.DeliveryAddress,
                IsDone = false,
                LeftText = DateTime.Now.ToString("d")
            }
        };

        public string StaffName => AuthService.CurrentUser().FirstName + AuthService.CurrentUser().LastName;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}