using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Pages.Agents
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Edit : PageModel
    {
        [BindProperty] public NewAgentDto UserData { get; set; }

        public void OnGet(Guid? id, [FromServices] AppDbContext dbContext)
        {
            if (id == null)
            {
                UserData = new NewAgentDto();
            }
            else
            {
                var user = dbContext.Users.Find(id.ToString());

                UserData = new NewAgentDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    AgentCode = user.AgentCode
                };
            }
        }

        public async Task<IActionResult> OnPostAsync([FromServices] AppDbContext dbContext, [FromServices] UserManager<AppUser> userManager)
        {
            if (!ModelState.IsValid) return Page();
            
            var user = new AppUser();
            var isEdit = false;

            if (UserData.Id != null && !UserData.Id.Equals(Guid.Empty.ToString()))
            {
                user = await userManager.FindByIdAsync(UserData.Id);
                user.FirstName = UserData.FirstName;
                user.LastName = UserData.LastName;
                user.AgentCode = UserData.AgentCode;
                isEdit = true;
            }
            else
            {
                user.Email = UserData.Email;
                user.UserName = UserData.Email;
                user.NormalizedUserName = UserData.Email.ToUpper();
                user.FirstName = UserData.FirstName;
                user.LastName = UserData.LastName;
                user.AgentCode = UserData.AgentCode;
            }

            if (isEdit)
            {
                await userManager.UpdateAsync(user);
            }
            else
            {
                var res = await userManager.CreateAsync(user, UserData.Password);
                if (!res.Succeeded)
                {
                    foreach (var identityError in res.Errors)
                    {
                        ModelState.AddModelError(identityError.Code, identityError.Description);
                    }
                }
            }

            await userManager.AddToRoleAsync(user, UserRole.Agent);

            return Redirect("/Agents");
        }
    }
}