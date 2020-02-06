using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Web.Pages.Agents
{
    public class Delete : PageModel
    {
        [BindProperty]
        public AppUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, [FromServices] UserManager<AppUser> userManager)
        {
            if (id.HasValue)
            {
                AppUser = await userManager.FindByIdAsync(id.Value.ToString());
                return Page();
            }
            else
            {
                return Redirect("./Index");
            }
        }

        public async Task<IActionResult> OnPostAsync([FromServices] UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByIdAsync(AppUser.Id);
            await userManager.DeleteAsync(user);
            return Redirect("./Index");
        }
    }
}