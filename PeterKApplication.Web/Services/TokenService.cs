using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.Web.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser appUser, double days = 30);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<string> CreateToken(AppUser appUser, double days = 30)
        {
            var user = _dbContext.Users.Find(appUser.Id);
            var business = _dbContext.Businesses.Find(user.BusinessId);
            
            Console.WriteLine("Returning token for user:" + JsonConvert.SerializeObject(new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.CountryCode,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                user.IsAutoSyncEnabled,
                bid = business.Id
            }));

            var identityOptions = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(identityOptions.ClaimsIdentity.UserIdClaimType, appUser.Id),
                new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, appUser.UserName?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.PrimarySid, appUser.Id.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Email, appUser.Email?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.GivenName, appUser.FirstName?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Surname, appUser.LastName?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Country, appUser.CountryCode?.ToString() ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber?.ToString() ?? string.Empty),
                new Claim("IsPhoneConfirmed", appUser.PhoneNumberConfirmed.ToString()),
                new Claim("IsAutoSyncEnabled", appUser.IsAutoSyncEnabled.ToString()),
                new Claim("UserId", user.Id),
                new Claim("BusinessId", business.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(appUser);
            claims.AddRange(roles.Select(role => new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role)));

            if ((await _userManager.IsInRoleAsync(appUser, UserRole.Owner)))
            {
                var bAdded = false;
                if (business?.IsAdded != null)
                    bAdded = business.IsAdded;
                var bVerified = false;
                if (business?.IsVerified != null)
                    bVerified = business.IsVerified;

                claims.Add(new Claim("IsAdded", bAdded.ToString()));
                claims.Add(new Claim("IsVerified", bVerified.ToString()));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(days),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha512Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}