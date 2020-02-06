using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSupportPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSupportPlansPage : ContentPage
    {
        private List<PlanCarouselOption> _planOptions;

        public OwnerBusinessTabSupportPlansPage()
        {
            InitializeComponent();
            
            PlanOptions = new List<PlanCarouselOption>
            {
                new PlanCarouselOption
                {
                    Title = "PLAN #1",
                    Features = new []
                    {
                        "Cloud + Sync",
                        "Analytics",
                        "Books",
                        "Shares",
                        "Coupons",
                        "Location",
                        "Staff"
                    }
                },
                new PlanCarouselOption
                {
                    Title = "PLAN #2",
                    Features = new []
                    {
                        "Cloud + Sync",
                        "Analytics",
                        "Books",
                        "Locations",
                        "Coupons",
                        "Staff"
                    }
                },
                new PlanCarouselOption
                {
                    Title = "PLAN #3",
                    Features = new []
                    {
                        "Cloud + Sync",
                        "Analytics",
                        "Books",
                        "Shares",
                        "Locations",
                        "Coupons",
                        "Staff"
                    }
                },
                new PlanCarouselOption
                {
                    Title = "PLAN #4",
                    Features = new []
                    {
                        "Cloud + Sync",
                        "Analytics",
                        "Books",
                        "Shares",
                        "Locations",
                        "Coupons",
                        "Staff"
                    }
                }
            };
        }

        public List<PlanCarouselOption> PlanOptions
        {
            get => _planOptions;
            set
            {
                _planOptions = value;
                OnPropertyChanged(nameof(PlanOptions));
            }
        }
    }
}