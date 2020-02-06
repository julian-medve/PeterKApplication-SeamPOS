using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages.Businesses
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Index : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Index(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Public properties
        public IList<Business> Businesses { get; set; }
        
        public async Task OnGetAsync()
        {
            Businesses = await _dbContext.Businesses.ToListAsync();
        }
    }
}