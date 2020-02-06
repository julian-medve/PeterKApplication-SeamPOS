using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface IBusinessService
    {
        Task<GetBusinessDto> GetBusiness();
        Task<Business> UpdateBusiness(UpdateBusinessDto updateBusiness);
    }

    public class BusinessService : IBusinessService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;

        public BusinessService(AppDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<GetBusinessDto> GetBusiness()
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            var business = await _dbContext.Businesses.FirstOrDefaultAsync(b => b.Id == currentUser.BusinessId);
            if (business == null)
            {
                throw new AppException("Business not found");
            }

            return new GetBusinessDto
            {
                OwnersDocumentImage = business.OwnersDocumentImage,
                BusinessDocumentImage = business.BusinessDocumentImage,
                Longitude = business.Longitude,
                Latitude = business.Latitude,
                BusinessLocation = business.BusinessLocation,
                BusinessSetup = business.BusinessSetup,
                Image = business.Image,
                BusinessBusinessCategories = business.BusinessBusinessCategories,
                BusinessRetailSubcategories = business.BusinessRetailSubcategories
            };
        }

        public async Task<Business> UpdateBusiness(UpdateBusinessDto updateBusiness)
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                throw new AppException("User not authenticated");
            }

            var business = await _dbContext.Businesses
                .FirstOrDefaultAsync(b => b.Id == currentUser.BusinessId);

            if (business == null)
            {
                throw new AppException("Business not found");
            }

            if (updateBusiness.Image != null)
            {
                business.Image = new ImageModel
                {
                    ImageData = updateBusiness.Image
                };
            }


            if (updateBusiness.BusinessDocumentImage != null)
            {
                business.BusinessDocumentImage = new ImageModel
                {
                    ImageData = updateBusiness.Image
                };
            }

            if (updateBusiness.OwnersDocumentImage != null)
            {
                business.OwnersDocumentImage = new ImageModel
                {
                    ImageData = updateBusiness.Image
                };
            }
            
            business.BusinessSetup = _dbContext.BusinessSetups.Find(updateBusiness.BusinessSetup);
            business.BusinessLocation = _dbContext.BusinessLocations.Find(updateBusiness.BusinessLocation);
            business.Latitude = updateBusiness.Latitude;
            business.Longitude = updateBusiness.Longitude;
            business.Name = business.Name;
            business.IsAdded = true;
            
            _dbContext.BusinessRetailSubcategories.RemoveRange(_dbContext.BusinessRetailSubcategories.Where(v => v.Business == business));
            business.BusinessBusinessCategories = new List<BusinessBusinessCategory>();
            foreach (var updateBusinessBusinessBusinessCategory in updateBusiness.BusinessBusinessCategories)
            {
                business.BusinessBusinessCategories.Add(new BusinessBusinessCategory
                {
                    Id = Guid.NewGuid(),
                    BusinessCategory = _dbContext.BusinessCategories.Find(updateBusinessBusinessBusinessCategory)
                });
            }
            
            _dbContext.BusinessBusinessCategories.RemoveRange(_dbContext.BusinessBusinessCategories.Where(v => v.Business == business));
            business.BusinessRetailSubcategories = new List<BusinessRetailSubcategory>();
            foreach (var updateBusinessRetailSubcategory in updateBusiness.BusinessRetailSubcategories)
            {
                business.BusinessRetailSubcategories.Add(new BusinessRetailSubcategory
                {
                    Id = Guid.NewGuid(),
                    RetailSubcategory = _dbContext.RetailSubcategories.Find(updateBusinessRetailSubcategory)
                });
            }

            await _dbContext.SaveChangesAsync();

            return _dbContext.Businesses
                .Include(b => b.BusinessBusinessCategories)
                    .ThenInclude(bc => bc.BusinessCategory)
                .Include(b => b.BusinessRetailSubcategories)
                .Include(b => b.Image)
                .Include(b => b.BusinessDocumentImage)
                .Include(b => b.OwnersDocumentImage)
                .Include(b => b.BusinessSetup)
                .Include(b => b.BusinessLocation)
                .Include(b => b.PaymentPlan)
                .Include(b => b.BusinessPaymentTypes)
                    .ThenInclude(p => p.PaymentType)
                .AsNoTracking()
                .First(f => f.Id == business.Id);
            
        }
    }
}