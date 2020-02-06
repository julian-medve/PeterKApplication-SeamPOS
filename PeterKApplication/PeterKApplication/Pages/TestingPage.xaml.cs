using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestingPage : ContentPage
    {
        private List<PairedListPair> _pairListItems = new List<PairedListPair>
        {
            new PairedListPair
            {
                Item1 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 1",
                    Image = "ImaePlacement.png"
                },
                Item2 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 2",
                    Image = "ImaePlacement.png"
                }
            },

            new PairedListPair
            {
                Item1 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 1",
                    Image = "ImaePlacement.png"
                },
                Item2 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 2",
                    Image = "ImaePlacement.png"
                }
            },

            new PairedListPair
            {
                Item1 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 1",
                    Image = "ImaePlacement.png"
                },
                Item2 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 2",
                    Image = "ImaePlacement.png"
                }
            },

            new PairedListPair
            {
                Item1 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 1",
                    Image = "ImaePlacement.png"
                },
                Item2 = new PairedListItem
                {
                    Id = Guid.NewGuid(),
                    Name = "PLIST 2",
                    Image = "ImaePlacement.png"
                }
            }
        };

        public TestingPage()
        {
            InitializeComponent();
        }

//        public List<OrderDefinition> Orders => new List<OrderDefinition>
//        {
//            new OrderDefinition
//            {
//                Amount = new decimal(104.1),
//                Date = DateTime.Now,
//                Status = OrderStatus.Pending,
//                Number = 1231233,
//                PaymentType = "Cash"
//            },
//            new OrderDefinition
//            {
//                Amount = new decimal(104.1),
//                Date = DateTime.Now,
//                Status = OrderStatus.Cancelled,
//                Number = 1231233,
//                PaymentType = "Card"
//            },
//            new OrderDefinition
//            {
//                Amount = new decimal(104.1),
//                Date = DateTime.Now,
//                Status = OrderStatus.Paid,
//                Number = 1231233,
//                PaymentType = "Cash"
//            }
//        };

        public List<VerticalConnectedProgressStep> VerticalSteps => new List<VerticalConnectedProgressStep>
        {
            new VerticalConnectedProgressStep
            {
                Title = "Order Prepared By",
                Subtitle = "Staff name",
                LeftText = "21 Mar at 09:00",
                IsDone = true
            },
            new VerticalConnectedProgressStep
            {
                Title = "Status",
                Subtitle = "Pending",
                LeftText = "21 Mar at 09:00",
                IsDone = false
            },
            new VerticalConnectedProgressStep
            {
                Title = "Location",
                Subtitle = "Location name",
                LeftText = "21 Mar at 09:00",
                IsDone = false
            }
        };

        public List<Product> Products => new List<Product>
        {
            new Product
            {
                Name = "Corolla",
                Description = "Toyota",
                Price = 5000,
                StockQuantity = 4,
                ImageModel = new ImageModel { ImageData = FakeByteArrayImage.GetFakeFromResource("PeterKApplication.fakeimage.png") }
            },
            new Product
            {
                Name = "Avensis",
                Description = "Toyota",
                Price = 5000,
                StockQuantity = 4,
                ImageModel = new ImageModel { ImageData = FakeByteArrayImage.GetFakeFromResource("PeterKApplication.fakeimage.png") }
            },
            new Product
            {
                Name = "RAV4",
                Description = "Toyota",
                Price = 5000,
                StockQuantity = 4,
                ImageModel = new ImageModel { ImageData = FakeByteArrayImage.GetFakeFromResource("PeterKApplication.fakeimage.png") }
            },
            new Product
            {
                Name = "Yaris",
                Description = "Toyota",
                Price = 5000,
                StockQuantity = 4,
                ImageModel = new ImageModel { ImageData = FakeByteArrayImage.GetFakeFromResource("PeterKApplication.fakeimage.png") }
            }
        };



        public List<PairedListPair> PairListItems
        {
            get => _pairListItems;
            set
            {
                _pairListItems = value;
                OnPropertyChanged(nameof(PairListItems));
            }
        }

        private void PairedList_OnListItemTapped(object sender, PairedListEventArgs e)
        {
            Console.WriteLine("Pair list item tapped:" + e.Item.Name);
        }
    }
}