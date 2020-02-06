using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;
using Sentry.Protocol;

namespace PeterKApplication.Web.Pages.Agents
{
    [AuthorizeRoles(UserRole.Administrator)]
    public class Index : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public Index(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IList<AppUser> Agents { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            Agents = await _userManager.GetUsersInRoleAsync(UserRole.Agent);

            if (!string.IsNullOrEmpty(SearchString))
            {
                Agents = Agents
                    .Where(a =>
                        a.UserName.ToUpper().Contains(SearchString.ToUpper()) ||
                        a.LastName.ToUpper().Contains(SearchString.ToUpper()) ||
                        a.FirstName.ToUpper().Contains(SearchString.ToUpper()))
                    .ToList();
            }
        }
    }
}