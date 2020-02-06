using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DotLiquid.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PeterKApplication.Data;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Enums;
using PeterKApplication.Shared.Extensions;
using PeterKApplication.Shared.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace PeterKApplication.Services
{
    public class SyncService
    {
        private readonly AuthService _authService;
        private readonly PrivateApiService _privateApiService;

        public SyncService(AuthService authService, PrivateApiService privateApiService)
        {
            _authService = authService;
            _privateApiService = privateApiService;

            new Thread(async () => await SyncAutomatic()).Start();
        }

        private async Task SyncAutomatic()
        {
            while (true)
            {
                Thread.Sleep(10000);
                if (ManualSync) continue;
                var shouldSync = _authService.AutoSync();
                var isDps = AuthService.IsOwner;

                if (shouldSync || isDps)
                {
                    await Sync();
                }
            }
        }

        public async Task SyncManual()
        {
            ManualSync = true;
            using (UserDialogs.Instance.Loading("Synchronizing data"))
            {
                await Sync();
            }

            ManualSync = false;
        }

        public bool ManualSync { get; set; }

        private async Task Sync()
        {
            var lastSync = Preferences.Get("LastSync", DateTime.MinValue);

            if (_authService.IsLoggedIn() && LoadingPage.DatabaseInitialized)
            {
                ApiExecutionResponse<SyncDto> ex;

                using (var db = new LocalDbContext())
                {
                    // fjdksljafklsdajlf

                    List<BusinessCategory> BusinessCategoryItems = db.BusinessCategories.ToList();

                    var orders = db.Orders.AsNoTracking().Include(o => o.OrderItems).Since(lastSync)
                        .ToList();

                    var orderDtos = orders
                        .Select(s => new OrderDto
                        {
                            Id = s.Id,
                            OrderNumber = s.OrderNumber,
                            DeliveryAddress = s.DeliveryAddress,
                            OrderedOn = s.OrderedOn,
                            OrderStatus = s.OrderStatus,
                            TransactionNumber = s.TransactionNumber,
                            AppUserId = s.AppUserId,
                            PaymentTypeId = s.PaymentTypeId,
                            ShippedOn = s.ShippedOn
                        }).ToList();
                    var orderItemDtos = orders.SelectMany(m => m.OrderItems).Select(s => new OrderItemDto
                    {
                        Id = s.Id,
                        Price = s.Price,
                        Quantity = s.Quantity,
                        OrderId = s.OrderId,
                        ProductId = s.ProductId
                    }).ToList();
                    var productDtos = db.Products
                        .AsNoTracking()
                        .Include(p => p.ImageModel)
                        .Since(lastSync)
                        .Select(p => new ProductDto
                        {
                            Description = p.Description,
                            Id = p.Id,
                            Name = p.Name,
                            Price = p.Price,
                            ImageModel = p.ImageModel,
                            StockQuantity = p.StockQuantity,
                            ProductCategoryId = p.ProductCategoryId
                        }).ToList();
                    var appUserDtos = db.Users
                        .AsNoTracking()
                        .Since(lastSync)
                        .Select(s => new AppUserDto
                        {
                            Email = s.Email,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            PhoneNumber = s.PhoneNumber,
                            Image = s.Image,
                            Id = s.Id,
                            Pin = s.Pin
                        }).ToList();
                    var productCategoryDtos = db.ProductCategories
                        .AsNoTracking()
                        .Since(lastSync)
                        .Select(s => new ProductCategoryDto
                        {
                            Id = s.Id,
                            Name = s.Name
                        }).ToList();
                    var paymentTypeDtos = db.BusinessPaymentTypes
                        .AsNoTracking()
                        .Since(lastSync)
                        .Select(s => new PaymentTypeDto
                        {
                            Id = s.Id
                        }).ToList();

                    var syncData = new SyncDto
                    {
                        OrderDtos = orderDtos,
                        OrderItemDtos = orderItemDtos,
                        ProductDtos = productDtos,
                        AppUserDtos = appUserDtos,
                        ProductCategoryDtos = productCategoryDtos,
                        LastSyncOn = lastSync,
                        PaymentTypeDtos = paymentTypeDtos,
                        ShouldIgnoreDataAmount = lastSync == DateTime.MinValue,
                    };

                    var b = db.Businesses
                        .Include(i => i.Image)
                        .FirstOrDefault(v => v.ModifiedOn > lastSync);

                    if (b != null)
                    {
                        syncData.BusinessDto = new BusinessDto
                        {
                            Id = b.Id,
                            Image = b.Image,
                            PaymentPlan = b.PaymentPlan
                        };
                    }

                    syncData.SyncStatusDto = new SyncStatusDto
                    {
                        HasBusiness = db.Businesses.Any(),
                        HasOrders = db.Orders.Any(),
                        HasProducts = db.Products.Any(),
                        HasAppUsers = db.Users.Any(),
                        HasOrderItems = db.OrderItems.Any(),
                        HasProductCategories = db.ProductCategories.Any(),
                        HasPaymentTypes = db.BusinessPaymentTypes.Any()
                    };

                    ex = await ApiHelper.Execute(_privateApiService.Client.Sync(syncData));
                }

                if (ex != null)
                {
                    using (var db = new LocalDbContext())
                    {
                        if (!ex.HasError)
                        {
                            if (ex.Response is
                            {
                                OrderDtos: var orders, 
                                ProductDtos: var products,
                                AppUserDtos: var users, 
                                ProductCategoryDtos: var categories, 
                                LastSyncOn: var lastSyncOn,
                                BusinessDto: var business,
                                ShouldIgnoreDataAmount: var b, 
                                OrderItemDtos: var orderItems, 
                                SyncStatusDto: var syncStatus, 
                                PaymentTypeDtos: var paymentTypes
                            })
                            {
                                Console.WriteLine("Sync model successful!");

                                try
                                {
                                    db.Businesses.Apply(business, (business1, b1) =>
                                    {
                                        business1.IsSynced = true;
                                        return business1;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.Users.ApplyUsers(users, (user, b1) =>
                                    {
                                        user.IsSynced = true;

                                        if (string.IsNullOrEmpty(user.UserName))
                                            user.UserName = user.FirstName + user.LastName;

                                        Console.WriteLine("dlsafjlas : " + user.Id);

                                        return user;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.Orders.Apply(orders, (order, b1) =>
                                    {
                                        order.IsSynced = true;
                                        return order;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.ProductCategories.Apply(categories, (category, b1) =>
                                    {
                                        category.IsSynced = true;
                                        return category;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.Products.Apply(products, (product, b1) =>
                                    {
                                        product.IsSynced = true;
                                        return product;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.OrderItems.Apply(orderItems, (item, b1) =>
                                    {
                                        item.IsSynced = true;
                                        return item;
                                    });

                                    /*await db.SaveChangesAsync();*/

                                    db.BusinessPaymentTypes.Apply(paymentTypes, (type, b1) =>
                                    {
                                        type.IsSynced = true;
                                        return type;
                                    });

                                    await db.SaveChangesAsync();
                                }
                                catch (Exception e) {
                                    Console.WriteLine("Exception while updating database : " + e.ToString());
                                }

                                Preferences.Set("LastSync", DateTime.Now);
                                Console.WriteLine("Sync successful!");
                            }
                            else
                            {
                                Console.WriteLine("Wrong sync response type!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("WE HAVE SYNC ERROR:" + ex.GeneralError);
                        }
                    }
                }
            }
        }
    }
}