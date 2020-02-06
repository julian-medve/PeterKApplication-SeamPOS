using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(UserRole.Owner, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        
        public SupportController(IMailService mailService, UserManager<AppUser> userManager, IAuthService authService)
        {
            _mailService = mailService;
            _userManager = userManager;
            _authService = authService;
        }
        
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendMessage([FromBody] SupportDto support)
        {
            var currentUser = await _authService.CurrentUser();
            if (currentUser == null)
            {
                return BadRequest("User not authenticated");
            }
            
            var admins = await _userManager.GetUsersInRoleAsync(UserRole.Administrator);

            foreach (var admin in admins)
            {
                _mailService.SendMail(admin, support.Subject, support.Message);
            }
            
            return Ok();
        }
    }
}