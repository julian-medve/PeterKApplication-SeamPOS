using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;
using RestSharp.Extensions;

namespace PeterKApplication.Web.Pages.KnowledgeBase
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Edit : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Edit(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Shared.Models.KnowledgeBase KnowledgeBase { get; set; }
        [BindProperty(SupportsGet = true)]
        public IFormFile UploadImage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                KnowledgeBase = new Shared.Models.KnowledgeBase();
                return Page();
            }

            KnowledgeBase = await _dbContext
                .KnowledgeBases
                .Include(k => k.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);

            if (KnowledgeBase == null)
            {
                return NotFound();
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            _dbContext.KnowledgeBases.Attach(KnowledgeBase);
            _dbContext.Entry(KnowledgeBase).Property(k => k.Title).IsModified = true;
            _dbContext.Entry(KnowledgeBase).Property(k => k.Description).IsModified = true;
            _dbContext.Entry(KnowledgeBase).Property(k => k.VideoUri).IsModified = true;
            if (UploadImage != null)
            {
                _dbContext
                    .Entry(KnowledgeBase)
                    .Reference(k => k.Image)
                    .IsModified = true;
                
                KnowledgeBase.Image = new ImageModel
                {
                    ImageData = UploadImage.OpenReadStream().ReadAsBytes()
                };
            
            }
        
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.KnowledgeBases.Any(k => k.Id == KnowledgeBase.Id))
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