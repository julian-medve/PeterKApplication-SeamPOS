using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Web.Services
{
    public interface IAuthService
    {
        Task<AppUser> CurrentUser();
        Task<string> RefreshToken();
    }
    
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AppUser> CurrentUser()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var userClaims = httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(userClaims);
        }

        public async Task<string> RefreshToken()
        {
            var user = await CurrentUser();

            return await _tokenService.CreateToken(user);
        }
    }
}