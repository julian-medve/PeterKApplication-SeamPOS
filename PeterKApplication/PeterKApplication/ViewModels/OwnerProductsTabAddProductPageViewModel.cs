using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerProductsTabAddProductPageViewModel : INotifyPropertyChanged
    {
        private Product _product = new Product
        {
            BusinessId = AuthService.BusinessId
        };


        private List<ProductCategory> _categories;
        private List<BusinessLocation> _locations;

        private string _selectedCategoryName;
        private string _selectedLocationName;

        public event PropertyChangedEventHandler PropertyChanged;

        
        private string _selectedCommissionPercentOption;
        private string _selectedCurrencyOption;


        public static OwnerProductsTabAddProductPageViewModel Self => null;


        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public String ProductCost 
        {
            get => Product.Cost == 0 ? null : Product.Cost + "";
            set 
            {
                Product.Cost = ParseToDecimal(value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public String ProductPrice
        {
            get => Product.Price == 0 ? null : Product.Price + "";
            set
            {
                Product.Price = ParseToDecimal(value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public String ProductQuantity
        {
            get => Product.StockQuantity == 0 ? null : Product.StockQuantity + "";
            set
            {
                Product.StockQuantity = ParseToInt(value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public string ProductWeight
        {
            get => Product.Weight == 0 ? null : Product.Weight + "";
            set
            {
                Product.Weight = ParseToDecimal(value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public string ProductDimension
        {
            get => Product.Dimension == 0 ? null : Product.Dimension + "";
            set
            {
                Product.Dimension = ParseToDecimal(value);
                OnPropertyChanged(nameof(Product));
            }
        }


        public String ProductCommission 
        {
            get => Product.Commission == 0 ? null : Product.Commission + "";
            set
            {
                Product.Commission = ParseToDecimal(value);
                OnPropertyChanged(nameof(Product));
            }
        }

        public int ParseToInt(string value)
        {
            if (value == null || value == "")
                return 0;
            else
                return int.Parse(value);
        }

        public decimal ParseToDecimal(string value)
        {
            if (value == null || value == "")
                return 0;
            else
                return decimal.Parse(value);
        }

        public List<string> CommissionPercentOptions => new List<string>
        {
            "0%",
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%" 
        };
        
        public string SelectedCommissionPercentOption 
        {
            get => _selectedCommissionPercentOption;
            set
            {
                _selectedCommissionPercentOption = value;
                OnPropertyChanged(SelectedCommissionPercentOption);
            }
        }


        /*public List<string> CurrencyOptions => new List<string>
        {
            "KES",
            "USD"
        };
*/

        /*public string SelectedCurrencyOption
        {
            get => _selectedCurrencyOption;
            set 
            {
                _selectedCurrencyOption = value;
                OnPropertyChanged(SelectedCurrencyOption);
            }
        }
*/

        public List<string> CategoryNames => Categories?.Select(c => c.Name)?.ToList();
        public List<string> LocationNames => Locations?.Select(c => c.Name)?.ToList();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetImage(byte[] eImage)
        {
            Product.ImageModel = new ImageModel
            {
                Id = Guid.NewGuid(),
                ImageData = eImage
            };
            OnPropertyChanged(nameof(Product));
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                Categories = db.ProductCategories.ToList();
                Locations = db.BusinessLocations.ToList();
            }
        }

        public List<ProductCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
                OnPropertyChanged(nameof(CategoryNames));
            }
        }

        public List<BusinessLocation> Locations
        {
            get => _locations;
            set 
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
                OnPropertyChanged(nameof(LocationNames));
            }
        }

        public string SelectedCategoryName
        {
            get => _selectedCategoryName;
            set
            {
                _selectedCategoryName = value;
                OnPropertyChanged(nameof(SelectedCategoryName));
            }
        }

        public string SelectedLocationName
        {
            get => _selectedLocationName;
            set 
            {
                _selectedLocationName = value;
                OnPropertyChanged(nameof(SelectedLocationName));
            }
        }

        public async Task Save()
        {
            using (var db = new LocalDbContext())
            {
                var cat         = db.ProductCategories.First(f => f.Name.ToLower().Equals(SelectedCategoryName.ToLower()));
                var location    = db.BusinessLocations.First(f => f.Name.ToLower().Equals(SelectedLocationName.ToLower()));

                Product.ProductCategoryId = cat.Id;
                Product.BusinessId = AuthService.BusinessId;
                Product.BusinessLocationId = location.Id;

                db.Products.Add(Product);
                await db.SaveChangesAsync();
            }
        }

        public async Task SaveAndNew()
        {
            await Save();

            Product = new Product
            {
                BusinessId = Product.BusinessId,
                Cost = Product.Cost,
                Description = Product.Description,
                Name = Product.Name,
                Price = Product.Price,
                IsMProduct = Product.IsMProduct,
                
                ImageModel = new ImageModel
                {
                    ImageData = Product.ImageModel?.ImageData
                },

                ProductCategoryId = Product.ProductCategoryId,
                StockQuantity = Product.StockQuantity,

                Commission = Product.Commission,
                CommissionPercent = Product.CommissionPercent,
                /*CommissionCurrencyFormat = Product.CommissionCurrencyFormat*/

                Weight = Product.Weight,
                Dimension = Product.Dimension,
            };
        }
    }
}