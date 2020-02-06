using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages.KnowledgeBase
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Index : PageModel
    {
        private readonly AppDbContext _dbContext;

        public Index(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Shared.Models.KnowledgeBase> KnowledgeBases { get; set; }

        public async Task OnGetAsync()
        {
            KnowledgeBases = await _dbContext
                .KnowledgeBases
                .Include(k => k.Image)
                .ToListAsync();
        }
    }
}