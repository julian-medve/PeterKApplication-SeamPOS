using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages.Businesses
{
    public class BusinessEdit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ImageModel OwnersDocument { get; set; }
        public ImageModel BusinessDocument { get; set; }
        public bool IsVerified { get; set; }
    }
    
    [AuthorizeRoles(UserRole.Administrator)]
    public class Edit : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Edit(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Business Business { get; set; }
        
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Business = await _dbContext
                .Businesses
                .Include(b => b.OwnersDocumentImage)
                .Include(b => b.BusinessDocumentImage)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (Business == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _dbContext.Businesses.Attach(Business);//.Property(b => b.IsVerified).IsModified = true;
            _dbContext.Entry(Business).Property(b => b.IsVerified).IsModified = true;
            
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Businesses.Any(b => b.Id == Business.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}