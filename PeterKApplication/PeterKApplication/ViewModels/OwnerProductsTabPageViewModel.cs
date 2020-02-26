using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerProductsTabPageViewModel : INotifyPropertyChanged
    {
        private string _search;
        private ProductCategory _selectedCategory;
        private List<Product> _products;
        private List<ProductCategory> _categories;
        private bool _hasAnyCategory;
        private bool _isAddProductMode;
        private bool _isCartMode;
        private bool _isShareMode;
        private bool _isOffersMode;
        private string _leftNavigationBarImage;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            ReloadAll();

            SetMode(AuthService.IsOwner ? ProductPageModes.AddProduct : ProductPageModes.CartMode);

            if (AuthService.IsOwner)
            {
                LeftNavigationBarImage = "Menu.png";
            }
            else
            {
                LeftNavigationBarImage = "";
            }
        }

        public void SetMode(ProductPageModes mode)
        {
            IsOffersMode = mode == ProductPageModes.OffersMode;
            IsShareMode = mode == ProductPageModes.ShareMode;
            IsCartMode = mode == ProductPageModes.CartMode;
            IsAddProductMode = mode == ProductPageModes.AddProduct;
        }

        public bool IsOffersMode
        {
            get => _isOffersMode;
            set
            {
                _isOffersMode = value;
                OnPropertyChanged(nameof(IsOffersMode));
            }
        }

        public bool IsShareMode
        {
            get => _isShareMode;
            set
            {
                _isShareMode = value;
                OnPropertyChanged(nameof(IsShareMode));
            }
        }

        public bool IsCartMode
        {
            get => _isCartMode;
            set
            {
                _isCartMode = value;
                OnPropertyChanged(nameof(IsCartMode));
            }
        }

        public void ReloadAll()
        {
            string currencyFormat = AuthService.CurrentUser().CurrencyFormat;

            using (var db = new LocalDbContext())
            {
                Products = db.Products.ToList();

                for (int i = 0; i < Products.Count; i++)
                {
                    Products[i].PriceLabel = currencyFormat + Products[i].Price;
                }

                Categories = db.ProductCategories.ToList();

                OnPropertyChanged(nameof(Products));
            }
        }

        public string CurrencyFormat => AuthService.CurrentUser().CurrencyFormat;


        public List<ProductCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                if (value.Any()) SelectedCategory = value.First();
                OnPropertyChanged(nameof(Categories));
                OnPropertyChanged(nameof(PairedCategories));
                OnPropertyChanged(nameof(HasAnyCategory));
            }
        }

        public List<PairedListPair> PairedCategories => Categories?.Aggregate(new List<PairedListPair>
        {
            new PairedListPair()
        }, (list, category) =>
        {
            var item = new PairedListItem
            {
                Id = category.Id,
                Name = category.Name
            };

            var c = list.Count - 1;

            if (list[c].Item1 == null)
            {
                list[c].Item1 = item;
            }
            else if (list[c].Item2 == null)
            {
                list[c].Item2 = item;
                list.Add(new PairedListPair());
            }

            return list;
        });

        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(FilteredProducts));
                OnPropertyChanged(nameof(MFilteredProducts));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(SelectedItems));
                OnPropertyChanged(nameof(HasSelectedItems));
                OnPropertyChanged(nameof(TotalQuantity));
            }
        }

        public static OwnerProductsTabPageViewModel Self => null;

        public List<Product> FilteredProducts {

            get {
                if (SelectedCategory == null)
                    return new List<Product>();

                if (SelectedCategory.Name == "All")
                    return string.IsNullOrEmpty(Search)
                                ? Products?.Where(p => p.IsMProduct == false).ToList()
                                : Products?.Where(v =>
                                    v.Name?.ToLower().Contains(Search.ToLower()) == true && v.IsMProduct == false).ToList();

                return string.IsNullOrEmpty(Search)
                    ? Products?.Where(p => p.ProductCategory == SelectedCategory && p.IsMProduct == false).ToList()
                    : Products?.Where(v =>
                        v.Name?.ToLower().Contains(Search.ToLower()) == true && v.ProductCategory == SelectedCategory && v.IsMProduct == false).ToList();
            }
        }

        public List<Product> MFilteredProducts {

            get
            {
                /*if (SelectedCategory.Name == "All")
                    return Products;*/

                return string.IsNullOrEmpty(Search)
                    ? Products?.Where(p => p.ProductCategory == SelectedCategory && p.IsMProduct == true).ToList()
                    : Products?.Where(v =>
                        v.Name?.ToLower().Contains(Search.ToLower()) == true && v.ProductCategory == SelectedCategory && v.IsMProduct == true).ToList();
            }
        }


        public string Amount
        {
            get
            {
                decimal amount = 0;
                for (int i = 0; i < MFilteredProducts.Count; i++)
                {
                    amount += MFilteredProducts[i].Price;
                }
                return amount + "";
            }
        }

        public ProductCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                OnPropertyChanged(nameof(FilteredProducts));
                OnPropertyChanged(nameof(MFilteredProducts));
            }
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                OnPropertyChanged(nameof(FilteredProducts));
                OnPropertyChanged(nameof(MFilteredProducts));
            }
        }

        public List<Product> SelectedItems => Products?.Where(v => v.IsSelected).ToList();
        public decimal TotalQuantity => SelectedItems?.Sum(s => s.Quantity) ?? 0;
        public decimal Total => Math.Round(SelectedItems?.Sum(s => s.Price * s.Quantity) ?? 0);

        public bool HasSelectedItems => SelectedItems?.Any() == true;

        public bool HasAnyCategory => Categories?.Any() == true;

        public bool IsAddProductMode
        {
            get => _isAddProductMode;
            set
            {
                _isAddProductMode = value;
                OnPropertyChanged(nameof(IsAddProductMode));
            }
        }

        public string LeftNavigationBarImage
        {
            get => _leftNavigationBarImage;
            set
            {
                _leftNavigationBarImage = value; 
                OnPropertyChanged(LeftNavigationBarImage);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ItemTapped(Product argsProduct)
        {
            Products = Products.Select(s =>
            {
                if (s.Id == argsProduct.Id)
                {
                    s.Quantity = argsProduct.Quantity;
                    s.IsSelected = s.Quantity > 0;
                }

                return s;
            }).ToList();
        }

        public void CategoryTapped(string Id)
        {
            SelectedCategory = Categories.First(c => c.Id.ToString() == Id);
        }

        public void ChangeCategory(TabDefinition eTab)
        {
            SelectedCategory = (ProductCategory) eTab.Object;
        }

        public void AddQuantity(Product argsProduct, decimal i)
        {
            Products = Products.Select(s =>
            {
                if (s.Id == argsProduct.Id)
                {
                    if (s.StockQuantity < s.Quantity) 
                    {
                        UserDialogs.Instance.Alert("There are less items than you selected.", "Alert", "OK");
                        s.Quantity = 0;
                        return s;
                    }

                    /*s.Quantity += i;*/
                    s.IsSelected = s.Quantity > 0;
                }
                return s;
            }).ToList();
        }

        public void AddOwnerProductQuantity(Product argsProduct, decimal i)
        {
            Products = Products.Select(s =>
            {
                if (s.Id == argsProduct.Id)
                {
                    s.StockQuantity += int.Parse(s.Quantity + "");
                    UpdateOwnerProduct(s);
                }

                return s;
            }).ToList();

            ReloadAll();
        }

        public async Task RemoveAllOwnerProductsQuantityAsync(Product argsProduct)
        {
            ConfirmConfig config = new ConfirmConfig();
            config.Message = "Are you sure to delete " + argsProduct.Name;
            config.UseYesNo(); 
            config.OkText = "Sure";
            config.CancelText = "Cancel";

            var result = await UserDialogs.Instance.ConfirmAsync(config);

            if(result)
            {
                Products = Products.Select(s =>
                {
                    if (s.Id == argsProduct.Id)
                    {
                        s.StockQuantity += int.Parse(s.Quantity + "");
                        RemoveOwnerProduct(s);
                    }

                    return s;
                }).ToList();

                ReloadAll();

            } 
        }

        public async Task UpdateOwnerProduct(Product product)
        {
            using (var db = new LocalDbContext())
            {
                db.Products.Update(product);
                await db.SaveChangesAsync();
            }

            ReloadAll();
        }

        public async Task RemoveOwnerProduct(Product product)
        {
            using (var db = new LocalDbContext())
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }

            ReloadAll();
        }


        public void RemoveAllQuantity(Product argsProduct)
        {
            Products = Products.Select(s =>
            {
                if (s.Id == argsProduct.Id)
                {
                    s.Quantity = 0;
                    s.IsSelected = false;
                }

                return s;
            }).ToList();
        }

        public void TapOperation(Product argsProduct)
        {
            Products = Products.Select(s =>
            {
                if (s.Id == argsProduct.Id)
                {
                    if (s.Quantity <= 0)
                    {
                        s.Quantity += 1;
                    }
                    else
                    {
                        s.Quantity -= 1;
                        s.Quantity = s.Quantity >= 0 ? s.Quantity : 0;
                    }

                    s.IsSelected = s.Quantity > 0;
                }

                return s;
            }).ToList();
        }

        public async Task AddCategory(string resText)
        {
            using (var db = new LocalDbContext())
            {
                db.ProductCategories.Add(new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    BusinessId = AuthService.BusinessId,
                    Name = resText
                });

                await db.SaveChangesAsync();
            }

            ReloadAll();
        }
    }

    public enum ProductPageModes
    {
        AddProduct,
        CartMode,
        ShareMode,
        OffersMode
    }
}