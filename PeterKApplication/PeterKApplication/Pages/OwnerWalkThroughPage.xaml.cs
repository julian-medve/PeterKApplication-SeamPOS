using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using PeterKApplication.Controls;
using PeterKApplication.Data;
using PeterKApplication.Extensions;
using PeterKApplication.Models;
using PeterKApplication.Pages.OwnerTabbedPages;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerWalkThroughPage : ContentPage
    {
        List<StackLayout> Steps;
        private int _currentStep = 0;
        private List<ChoiceRingItemGroup> _businessOperationItems;
        private List<ChoiceRingItemGroup> _businessCategoryItems;
        private List<ChoiceRingItemGroup> _businessSubCategoryItems;
        private List<ChoiceRingItemGroup> _businessSetupItems;

        public OwnerWalkThroughPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<OwnerWalkThroughPageViewModel>();

            Steps = new List<StackLayout>
            {
//                BusinessOperation,
                BusinessCategory,
//                BusinessSubCategory,
                BusinessSetup,
                BusinessConfirmation,
                BusinessLocation
            };

//            BusinessOperationItems = new List<ChoiceRingItemGroup>
//            {
//                new ChoiceRingItemGroup
//                {
//                    Item1 = new ChoiceRingItem("PHYSICAL LOCATION"),
//                    Item3 = new ChoiceRingItem("ONLINE")
//                }
//            };

            using (var db = new LocalDbContext())
            {
                BusinessCategoryItems = db.BusinessCategories.ToList().Select(s => new ChoiceRingItemTemplate
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToChoiceRingItemGroup();

                BusinessSetupItems = db.BusinessSetups.ToList().Select(s => new ChoiceRingItemTemplate
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToChoiceRingItemGroup();
            }

            ShowStep();
        }

        public List<ChoiceRingItemGroup> BusinessSetupItems
        {
            get => _businessSetupItems;
            set
            {
                _businessSetupItems = value;
                OnPropertyChanged(nameof(BusinessSetupItems));
                OnPropertyChanged(nameof(BusinessConfirmationItems));
            }
        }

        public List<ChoiceRingItemGroup> BusinessSubCategoryItems
        {
            get => _businessSubCategoryItems;
            set
            {
                _businessSubCategoryItems = value;
                OnPropertyChanged(nameof(BusinessSubCategoryItems));
            }
        }

        public List<ChoiceRingItemGroup> BusinessCategoryItems
        {
            get => _businessCategoryItems;
            set
            {
                _businessCategoryItems = value;
                OnPropertyChanged(nameof(BusinessCategoryItems));
                BusinessSubCategoryItems = GenerateSubCategoryItems();
            }
        }

        private List<ChoiceRingItemGroup> GenerateSubCategoryItems()
        {
            var list = new List<ChoiceRingItemGroup>();

            var categories = BusinessCategoryItems?.SelectedNames() ?? new List<string>();

            if (categories.Contains("Automotive"))
            {
                list.Add(new ChoiceRingItem
                {
                    Text = "Car Accessories",
                    Id = Guid.Parse("5246CF01-468E-4861-AB37-13DC1232CC1F")
                });

                list.Add(new ChoiceRingItem
                {
                    Text = "Garage",
                    Id = Guid.Parse("A154D280-3627-46D4-A8D0-C3A698B8E578")
                });
            }

            if (categories.Contains("Business Services"))
            {
                list.Add(new ChoiceRingItem
                {
                    Text = "Printing",
                    Id = Guid.Parse("D50D730D-BE27-4055-82E7-83A469B4E3FA")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Computer Services",
                    Id = Guid.Parse("39A09A44-2BE2-4A28-863E-5B6D237FFA28")
                });
            }

            if (categories.Contains("Retail"))
            {
                list.Add(new ChoiceRingItem
                {
                    Text = "Apparels Clothes",
                    Id = Guid.Parse("F38992C0-CFF5-4267-A4C5-7AEFAA284E31")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Computer Accessories",
                    Id = Guid.Parse("1805BD8B-48B7-4E03-8677-C05732194D1D")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Electronics",
                    Id = Guid.Parse("1AC64278-6CDB-4303-B8E8-C23F865F7D70")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Education And Books",
                    Id = Guid.Parse("0655627C-CFF5-42A1-B3A3-0D99C7AA288A")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Mobile Phones And Accessories",
                    Id = Guid.Parse("A4FD1D1E-D92F-4E2B-B1D4-5DC44EBE0815")
                });
                list.Add(new ChoiceRingItem
                {
                    Text = "Home And Furniture",
                    Id = Guid.Parse("8298309B-3D3B-4C03-8BF3-1F4D4B14D870")
                });
            }

            return list;
        }

        public List<ChoiceRingItemGroup> BusinessOperationItems
        {
            get => _businessOperationItems;
            set
            {
                _businessOperationItems = value;
                OnPropertyChanged(nameof(BusinessOperationItems));
            }
        }

        public bool IsNotFinalStep => Steps == null || _currentStep != Steps.Count - 1;

        private void ShowStep()
        {
            Steps.ForEach(s => s.IsVisible = false);
            Steps[_currentStep].IsVisible = true;
            OnPropertyChanged(nameof(IsNotFinalStep));

            var selected = BusinessSetupItems?.FirstSelectedId();
            var limitedCompany = BusinessSetupItems?.First()?.Item3?.Id;

            var isSelected = selected?.Equals(limitedCompany) == true;

            PlusImage.HeightRequest = isSelected ? 50 : 0;
            PlusImage.IsVisible = isSelected;
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            if (_currentStep == 0) return;
            _currentStep -= 1;
            ShowStep();
        }

        private async void OnSkipTapped(object sender, EventArgs e)
        {
            if (_currentStep == Steps.Count - 1)
            {
                await SaveBusinessData();
                return;
            }

            _currentStep += 1;
            ShowStep();
        }

        private async void OnNextStepClicked(object sender, EventArgs e)
        {
            if (_currentStep == Steps.Count - 1)
            {
                await SaveBusinessData();
                return;
            }

            _currentStep += 1;
            ShowStep();
        }

        private async void AddLocationClicked(object sender, EventArgs e)
        {
            await SaveBusinessData();
        }

        private async Task SaveBusinessData()
        {
            var bc = BindingContext.As<OwnerWalkThroughPageViewModel>();

            var retailSub = BusinessSubCategoryItems?.SelectedIds()?.ToList();
            var businessSetup = BusinessSetupItems?.FirstSelectedId();
            var bCategories = BusinessCategoryItems?.SelectedIds()?.ToList();
//                var bLocation = db.BusinessLocations.First(f => f.Name == BusinessLocation); 
            var bImage = DocumentImage;

            var req = new UpdateBusinessDto
            {
                BusinessRetailSubcategories = retailSub,
                BusinessSetup = businessSetup,
                BusinessBusinessCategories = bCategories,
                BusinessLocation = null,
                BusinessDocumentImage = bImage
            };

            Console.WriteLine("Sending:" + JsonConvert.SerializeObject(req));

            if (await bc.UploadBusinessSettings(req))
            {
                Application.Current.MainPage = new OwnerMasterDetailPage();
            }
        }

        private async void PairedList_OnListItemTapped(object sender, PairedListEventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                UserDialogs.Instance.Alert("Warning", "Changing image requires camera feature!", "Ok");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                MaxWidthHeight = 500,
                PhotoSize = PhotoSize.MaxWidthHeight
            });

            if (file == null)
                return;

            var ms = new MemoryStream();

            file.GetStream().CopyTo(ms);

            DocumentImage = ms.ToArray();
        }

        public byte[] DocumentImage { get; set; }

        public List<PairedListPair> BusinessConfirmationItems
        {
            get
            {
                var selected = BusinessSetupItems?.FirstSelectedId();
                var proprietor = BusinessSetupItems?.First()?.Item1?.Id;
                var partnership = BusinessSetupItems?.First()?.Item2?.Id;
                var limitedCompany = BusinessSetupItems?.First()?.Item3?.Id;

                if (selected?.Equals(proprietor) == true)
                {
                    return new List<PairedListPair>
                    {
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "ID OR PASSPORT",
                                Image = "ImaePlacement.png"
                            },
                            Item2 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "BUSINESS PERMIT",
                                Image = "ImaePlacement.png"
                            }
                        },
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "REGISTRATION CERTIFICATE",
                                Image = "ImaePlacement.png"
                            }
                        }
                    };
                }

                if (selected?.Equals(partnership) == true)
                {
                    return new List<PairedListPair>
                    {
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "ID OR PASSPORT",
                                Image = "ImaePlacement.png"
                            },
                            Item2 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "ID OR PASSPORT",
                                Image = "ImaePlacement.png"
                            }
                        },
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "REGISTRATION CERTIFICATE",
                                Image = "ImaePlacement.png"
                            },
                            Item2 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "BUSINESS PERMIT",
                                Image = "ImaePlacement.png"
                            }
                        }
                    };
                }

                if (selected?.Equals(limitedCompany) == true)
                {
                    return new List<PairedListPair>
                    {
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "ID OR PASSPORT",
                                Image = "ImaePlacement.png"
                            },
                            Item2 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "ID OR PASSPORT",
                                Image = "ImaePlacement.png"
                            }
                        },
                        new PairedListPair
                        {
                            Item1 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "REGISTRATION CERTIFICATE",
                                Image = "ImaePlacement.png"
                            },
                            Item2 = new PairedListItem
                            {
                                Id = Guid.NewGuid(),
                                Name = "BUSINESS PERMIT",
                                Image = "ImaePlacement.png"
                            }
                        }
                    };
                }

                return null;
            }
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            PairedList_OnListItemTapped(sender, null);
        }
    }
}