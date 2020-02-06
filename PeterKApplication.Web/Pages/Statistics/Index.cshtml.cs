using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Auth;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Pages.Statistics
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Index : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Index(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SelectList Periods { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedPeriod { get; set; }

        public decimal TotalTransactions { get; set; }
        public decimal HighestTransaction { get; set; }
        public decimal LowestTransaction { get; set; }
        
        public async Task OnGetAsync()
        {
            
            Periods = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "Today", Value = "1"},
                new SelectListItem {Text = "This Week", Value = "2"},
                new SelectListItem {Text = "This Month", Value = "3"},
                new SelectListItem {Text = "This Year", Value = "4"}
            }, "Value", "Text", "3");

            if (string.IsNullOrEmpty(SelectedPeriod)) SelectedPeriod = "3";

            DateTime searchFrom;
            
            if (SelectedPeriod.Equals("1"))
            {
                searchFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else if (SelectedPeriod.Equals("2"))
            {
                searchFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                while (CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek != searchFrom.DayOfWeek)
                {
                    searchFrom = searchFrom.AddDays(-1);
                }
            }
            else if (SelectedPeriod.Equals("3"))
            {
                searchFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            else
            {
                searchFrom = new DateTime(DateTime.Now.Year, 1, 1);
            }


            var orders = await _dbContext
                .Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderedOn >= searchFrom)
                .ToListAsync();

            if (orders.Count > 0)
            {
                LowestTransaction = HighestTransaction = orders[0].Amount;
            }
            
            foreach (var o in orders)
            {
                TotalTransactions += o.Amount;

                LowestTransaction = o.Amount < LowestTransaction ? o.Amount : LowestTransaction;
                HighestTransaction = o.Amount > HighestTransaction ? o.Amount : HighestTransaction;
            }
        }
    }
}