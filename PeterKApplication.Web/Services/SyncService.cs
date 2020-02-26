using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Extensions;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface ISyncService
    {
        Task<SyncDto> Sync(SyncDto sync);
        Task<SyncDetailsDto> SyncDetails(SyncDetailsDto syncDetails);
    }

    public class SyncService : ISyncService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;

        public SyncService(AppDbContext dbContext, UserManager<AppUser> userManager, IAuthService authService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<SyncDto> Sync(SyncDto sync)
        {
            var currentUser = await _authService.CurrentUser();
            var currentBusinessId = currentUser.BusinessId;
            var lastSync = sync.LastSyncOn;

            // Add Users
            _dbContext.Users.ApplyUsers(sync.AppUserDtos, (user, isNew) =>
            {
                user.BusinessId = currentBusinessId;
                return user;
            });

            // Add ProductCategories
            _dbContext.ProductCategories.Apply(sync.ProductCategoryDtos, (productCategory, isNew) =>
            {
                productCategory.BusinessId = currentBusinessId;
                return productCategory;
            });

            // Add Products
            _dbContext.Products.Apply(sync.ProductDtos, (product, isNew) =>
            {
                product.BusinessId = currentBusinessId;
                return product;
            });

            // Add Orders
            _dbContext.Orders.Apply(sync.OrderDtos, (order, isNew) =>
            {
                order.BusinessId = currentBusinessId;
                if (isNew) order.AppUserId = currentUser.Id;
                return order;
            });

            // Add Order items
            _dbContext.OrderItems.Apply(sync.OrderItemDtos, (item, isNew) =>
            {
                item.ProductId = item.ProductId;
                return item;
            });

            _dbContext.Businesses.Apply(sync.BusinessDto);

//            _dbContext.BusinessPaymentTypes.Apply(sync.PaymentTypeDtos, (type, b) =>
//            {
//                type.BusinessId = currentBusinessId;
//                return type;
//            });

            // Save changes to database
            await _dbContext.SaveChangesAsync();

            // Get all new entries which will be returned to client before adding his entries

            // TODO - currently returns all users, but should return only new users
            var newAppUsers = _dbContext.Users
                .Since(lastSync, sync.SyncStatusDto.HasAppUsers)
                .Where(u => u.BusinessId == currentBusinessId).ToList();

            var newProductCategories = _dbContext.ProductCategories
                .Since(lastSync, sync.SyncStatusDto.HasProductCategories)
                .Where(pc => pc.BusinessId == currentBusinessId).ToList();

            var newProducts = _dbContext.Products
                .Since(lastSync, sync.SyncStatusDto.HasProducts)
                .Where(p => p.BusinessId == currentBusinessId).ToList();

            var newOrders = _dbContext.Orders
                .Include(o => o.OrderItems)
                .Since(lastSync, sync.SyncStatusDto.HasOrders)
                .Where(o => o.BusinessId == currentBusinessId).ToList();

            var newBusiness = _dbContext.Businesses
                .Include(o => o.Image)
                .Include(o => o.PaymentPlan)
                .Since(lastSync, sync.SyncStatusDto.HasBusiness)
                .FirstOrDefault(b => b.Id == currentBusinessId);

            var newPaymentTypes = _dbContext.BusinessPaymentTypes
                .Since(lastSync, sync.SyncStatusDto.HasPaymentTypes)
                .Where(o => o.BusinessId == currentBusinessId).ToList();

            // Return SyncDto with every entry after LastSyncOn datetime

            var syncRet = new SyncDto
            {
                AppUserDtos = newAppUsers.Select(u => new AppUserDto
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        Id = u.Id,
                        Image = u.Image,
                        Pin = u.Pin
                    })
                    .ToList(),

                ProductCategoryDtos = newProductCategories.Select(c => new ProductCategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList(),
                ProductDtos = newProducts.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        ProductCategoryId = p.ProductCategoryId
                    })
                    .ToList(),
                OrderDtos = newOrders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    OrderStatus = o.OrderStatus,
                    OrderedOn = o.OrderedOn,
                    ShippedOn = o.ShippedOn.GetValueOrDefault(),
                    DeliveryAddress = o.DeliveryAddress,
                    TransactionNumber = o.TransactionNumber,
                    AppUserId = o.AppUserId,
                    PaymentTypeId = o.PaymentTypeId,

                    // Added by Mikhaylov 

                    Reference = o.Reference,
                }).ToList(),
                OrderItemDtos = newOrders.SelectMany(o => o.OrderItems).Select(s => new OrderItemDto
                {
                    Id = s.Id,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    OrderId = s.OrderId,
                    ProductId = s.ProductId
                }).ToList(),
                PaymentTypeDtos = newPaymentTypes.Select(s => new PaymentTypeDto
                {
                    Id = s.Id
                }).ToList()
            };

            if (newBusiness != null)
            {
                syncRet.BusinessDto = new BusinessDto
                {
                    Id = newBusiness.Id,
                    Image = newBusiness.Image,
                    Name = newBusiness.Name,
                    PaymentPlan = newBusiness.PaymentPlan
                };
            }

            if (!sync.ShouldIgnoreDataAmount)
            {
                var syncData = new Sync
                {
                    SyncedOn = lastSync,
                    DataAmount = syncRet.ToString().Length,
                    BusinessId = currentBusinessId
                };

                _dbContext.Syncs.Add(syncData);
                await _dbContext.SaveChangesAsync();
            }

            return syncRet;
        }

        public async Task<SyncDetailsDto> SyncDetails(SyncDetailsDto syncDetails)
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            var syncs = _dbContext
                .Syncs
                .Where(s =>
                    s.BusinessId == currentUser.BusinessId &&
                    s.SyncedOn.Month == syncDetails.Month &&
                    s.SyncedOn.Year == syncDetails.Year)
                .ToList();

            long dataAmount = 0;
            syncs.ForEach(s => dataAmount += s.DataAmount);

            return new SyncDetailsDto
            {
                Month = syncDetails.Month,
                Year = syncDetails.Year,
                DataAmount = dataAmount
            };
        }
    }
}