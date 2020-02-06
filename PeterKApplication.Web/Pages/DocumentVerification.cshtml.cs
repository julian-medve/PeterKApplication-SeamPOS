using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class DocumentVerification : PageModel
    {
        private readonly AppDbContext _dbContext;

        public DocumentVerification(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public IList<Business> Businesses { get; set; }

        public async Task OnGetAsync()
        {
            Businesses = await _dbContext
                .Businesses
                .Include(b => b.OwnersDocumentImage)
                .Include(b => b.BusinessDocumentImage)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task OnPostAsync(string id, bool isBusinessVerified)
        {
            Console.WriteLine("Id: " + id + ", IsBusinessVerified: " + isBusinessVerified);
            Console.WriteLine("Id: " + id + ", IsBusinessVerified: " + isBusinessVerified);
            Console.WriteLine("Id: " + id + ", IsBusinessVerified: " + isBusinessVerified);
            Console.WriteLine("Id: " + id + ", IsBusinessVerified: " + isBusinessVerified);
            var business = _dbContext.Businesses.FirstOrDefault(b => b.Id.ToString() == id);
            if (business == null)
            {
                return;
            }
            business.IsVerified = !business.IsVerified;
            await _dbContext.SaveChangesAsync();

            await OnGetAsync();
        }
    }
}