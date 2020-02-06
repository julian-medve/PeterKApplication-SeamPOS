using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages.KnowledgeBase
{ 
    [AuthorizeRoles(UserRole.Administrator)]
    public class Delete : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Delete(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Shared.Models.KnowledgeBase KnowledgeBase { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KnowledgeBase = await _dbContext
                .KnowledgeBases
                .Include(k => k.Image)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (KnowledgeBase == null)
            {
                return NotFound();
            }
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KnowledgeBase = await _dbContext.KnowledgeBases.FindAsync(id);

            if (KnowledgeBase != null)
            {
                _dbContext.KnowledgeBases.Remove(KnowledgeBase);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}