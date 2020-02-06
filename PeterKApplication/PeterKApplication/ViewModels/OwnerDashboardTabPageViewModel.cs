using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using Xamarin.Forms;

namespace PeterKApplication.ViewModels
{
    public class OwnerDashboardTabPageViewModel : INotifyPropertyChanged
    {
        private bool _hasFirstSale;
        private List<OverviewDataCenterDefinition> _salesData;
        private List<OverviewDataCenterDefinition> _offersData;
        private List<OverviewDataCenterDefinition> _revenueData;
        private OverviewDataSummaryDefinition _salesSummary;
        private OverviewDataSummaryDefinition _offersSummary;
        private OverviewDataSummaryDefinition _revenueSummary;
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerDashboardTabPageViewModel Self => null;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            using (var dbContext = new LocalDbContext())
            {
                HasFirstSale = dbContext.Orders.Any();

                var blue = StaticResourceHelper.Get<Color>("MainBlueColor");
                var green = StaticResourceHelper.Get<Color>("Green");

                SalesData = new List<OverviewDataCenterDefinition>
                {
                    new OverviewDataCenterDefinition
                    {
                        Title = "Total Sales",
                        CenterColor = blue,
                        CenterValue = dbContext.Orders.Include(o => o.OrderItems)
                            .Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity))
                    }
                };

                OffersData = new List<OverviewDataCenterDefinition>
                {
                    new OverviewDataCenterDefinition
                    {
                        Title = "Offers sold",
                        CenterColor = green,
                        CenterValue = 0
                    }
                };

                RevenueData = new List<OverviewDataCenterDefinition>
                {
                    new OverviewDataCenterDefinition
                    {
                        Title = "Total revenue",
                        CenterColor = blue,
                        CenterValue = dbContext.Orders.Include(o => o.OrderItems)
                            .Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity))
                    },
                    new OverviewDataCenterDefinition
                    {
                        Title = "Cash",
                        CenterColor = green,
                        CenterValue = dbContext.Orders.Include(o => o.OrderItems)
                            .Where(v => v.PaymentType.Name == "Cash")
                            .Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity))
                    },
                    new OverviewDataCenterDefinition
                    {
                        Title = "Mobile Money",
                        CenterColor = blue,
                        CenterValue = dbContext.Orders.Include(o => o.OrderItems)
                            .Where(v => v.PaymentType.Name != "Cash")
                            .Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity))
                    }
                };

                var last7days = DateTime.Now.Subtract(TimeSpan.FromDays(7));
                var last30days = DateTime.Now.Subtract(TimeSpan.FromDays(30));

                SalesSummary = new OverviewDataSummaryDefinition
                {
                    Last7Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last7days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Last30Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last30days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity))
                };

                OffersSummary = new OverviewDataSummaryDefinition
                {
                    Last7Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last7days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Last30Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last30days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity))
                };
                
                RevenueSummary = new OverviewDataSummaryDefinition
                {
                    Last7Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last7days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity)),
                    Last30Days = dbContext.Orders.Include(o => o.OrderItems).Where(v => v.CreatedOn > last30days)
                        .Sum(s => s.OrderItems.Sum(i => i.Price * i.Quantity))
                };
            }
        }

        public bool HasFirstSale
        {
            get => _hasFirstSale;
            set
            {
                _hasFirstSale = value;
                OnPropertyChanged(nameof(HasFirstSale));
            }
        }

        public List<OverviewDataCenterDefinition> SalesData
        {
            get => _salesData;
            set
            {
                _salesData = value;
                OnPropertyChanged(nameof(SalesData));
            }
        }

        public List<OverviewDataCenterDefinition> OffersData
        {
            get => _offersData;
            set
            {
                _offersData = value;
                OnPropertyChanged(nameof(OffersData));
            }
        }

        public List<OverviewDataCenterDefinition> RevenueData
        {
            get => _revenueData;
            set
            {
                _revenueData = value;
                OnPropertyChanged(nameof(RevenueData));
            }
        }

        public OverviewDataSummaryDefinition SalesSummary
        {
            get => _salesSummary;
            set
            {
                _salesSummary = value;
                OnPropertyChanged(nameof(SalesSummary));
            }
        }

        public OverviewDataSummaryDefinition OffersSummary
        {
            get => _offersSummary;
            set
            {
                _offersSummary = value;
                OnPropertyChanged(nameof(OffersSummary));
            }
        }

        public OverviewDataSummaryDefinition RevenueSummary
        {
            get => _revenueSummary;
            set
            {
                _revenueSummary = value;
                OnPropertyChanged(nameof(RevenueSummary));
            }
        }
    }
}