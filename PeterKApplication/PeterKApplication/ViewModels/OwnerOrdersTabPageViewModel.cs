using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Extensions;
using PeterKApplication.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerOrdersTabPageViewModel : INotifyPropertyChanged
    {
        private OrdersOverviewDataDefinition _todayData;
        private OrdersOverviewDataDefinition _last3DaysData;
        private OrdersOverviewDataDefinition _weekData;
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerOrdersTabPageViewModel Self => null;

        public OrdersOverviewDataDefinition TodayData
        {
            get => _todayData;
            set
            {
                _todayData = value;
                OnPropertyChanged(nameof(TodayData));
            }
        }

        public OrdersOverviewDataDefinition Last3DaysData
        {
            get => _last3DaysData;
            set
            {
                _last3DaysData = value;
                OnPropertyChanged(nameof(Last3DaysData));
            }
        }

        public OrdersOverviewDataDefinition WeekData
        {
            get => _weekData;
            set
            {
                _weekData = value;
                OnPropertyChanged(nameof(WeekData));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                var dnow = DateTime.Now;
                var startOfDay = new DateTime(dnow.Year, dnow.Month, dnow.Day, 0, 0, 0);
                dnow = dnow.Subtract(TimeSpan.FromDays(3));
                var last3DaysStartOfDay = new DateTime(dnow.Year, dnow.Month, dnow.Day, 0, 0, 0);
                dnow = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                var weekStartOfDay = new DateTime(dnow.Year, dnow.Month, dnow.Day, 0, 0, 0);

                TodayData = new OrdersOverviewDataDefinition
                {
                    Amount = db.Orders.Include(o => o.OrderItems).Where(o => o.CreatedOn >= startOfDay)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Highest = 144,
                    Lowest = 83,
                    Quantity = db.Orders.Count(o => o.CreatedOn >= startOfDay)
                };

                Last3DaysData = new OrdersOverviewDataDefinition
                {
                    Amount = db.Orders.Include(o => o.OrderItems).Where(o => o.CreatedOn >= last3DaysStartOfDay)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Highest = 144,
                    Lowest = 83,
                    Quantity = db.Orders.Count(o => o.CreatedOn >= last3DaysStartOfDay)
                };

                WeekData = new OrdersOverviewDataDefinition
                {
                    Amount = db.Orders.Include(o => o.OrderItems).Where(o => o.CreatedOn >= weekStartOfDay)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Highest = 144,
                    Lowest = 83,
                    Quantity = db.Orders.Count(o => o.CreatedOn >= weekStartOfDay)
                };
            }
        }
    }
}