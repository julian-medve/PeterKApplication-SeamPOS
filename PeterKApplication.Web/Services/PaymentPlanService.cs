using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface IPaymentPlanService
    {
        ICollection<PaymentPlanDto> GetPaymentPlans();
        Task AddPaymentPlan(PaymentPlanDto paymentPlan);
        Task UpdatePaymentPlan(PaymentPlanDto paymentPlan);
    }
    
    /*
     *
     * public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CloudSyncMBs { get; set; }
        public int AdditionalStaff { get; set; }
        public int Locations { get; set; }
        public decimal PaymentOption { get; set; }
     */
    
    public class PaymentPlanService : IPaymentPlanService
    {
        private readonly AppDbContext _dbContext;

        public ICollection<PaymentPlanDto> GetPaymentPlans()
        {
            var paymentPlans = _dbContext.PaymentPlans.ToList();

            return paymentPlans.Select(p => new PaymentPlanDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CloudSyncMBs = p.CloudSyncMBs,
                AdditionalStaff = p.AdditionalStaff,
                Locations = p.Locations,
                PaymentOption = p.PaymentOption
            }).ToList();
        }

        public async Task AddPaymentPlan(PaymentPlanDto paymentPlan)
        {
            var p = new PaymentPlan
            {
                Name = paymentPlan.Name,
                Price = paymentPlan.Price,
                CloudSyncMBs = paymentPlan.CloudSyncMBs,
                AdditionalStaff = paymentPlan.AdditionalStaff,
                Locations = paymentPlan.Locations,
                PaymentOption = paymentPlan.PaymentOption
            };

            await _dbContext.PaymentPlans.AddAsync(p);
        }

        public async Task UpdatePaymentPlan(PaymentPlanDto paymentPlan)
        {
            var pp = await _dbContext.PaymentPlans.FirstOrDefaultAsync(p => p.Id == paymentPlan.Id);
            if (pp == null)
            {
                throw new AppException("Payment plan not found");
            }

            pp.Name = paymentPlan.Name;
            pp.Price = paymentPlan.Price;
            pp.CloudSyncMBs = paymentPlan.CloudSyncMBs;
            pp.AdditionalStaff = paymentPlan.AdditionalStaff;
            pp.Locations = paymentPlan.Locations;
            pp.PaymentOption = paymentPlan.PaymentOption;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}