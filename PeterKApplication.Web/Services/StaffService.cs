using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;
using PeterKApplication.Web.Exceptions;

namespace PeterKApplication.Web.Services
{
    public interface IStaffService
    {
        Task<AuthStaffResDto> AuthenticateAsync(AuthStaffReqDto authStaffReq);
        Task RegisterAsync(RegisterStaffReqDto registerStaffReqDto);
        Task UpdateAsync(UpdateStaffReqDto updateStaffReqReq);
        Task DeleteAsync(DeleteStaffReqDto deleteStaffReq);
        Task<ICollection<GetStaffResDto>> GetStaffsAsync();
    }
    
    public class StaffService : IStaffService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public StaffService(AppDbContext dbContext, UserManager<AppUser> userManager, IAuthService authService, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _authService = authService;
            _tokenService = tokenService;
        }
        
        public async Task<AuthStaffResDto> AuthenticateAsync(AuthStaffReqDto authStaffReq)
        {
            var tokenUser = await _authService.CurrentUser();
            if (tokenUser == null)
            {
                return null;
            }
            
            var appUser = _dbContext.Users.SingleOrDefault(u => 
                u.Pin == authStaffReq.Pin && u.BusinessId == tokenUser.BusinessId);
            if (appUser == null)
            {
                return null;
            }

            return new AuthStaffResDto
            {
                StaffToken = await _tokenService.CreateToken(appUser)
            };
        }
        
        [AuthorizeRoles(UserRole.Owner)]
        public async Task RegisterAsync(RegisterStaffReqDto registerStaffReqDto)
        {
            var currentUser = await _authService.CurrentUser();

            if(currentUser == null)
            {
                throw new AppException("User authentication failed");
            }

            var userWithThatPin = await _dbContext.Users.FirstOrDefaultAsync(u => u.BusinessId == currentUser.BusinessId && u.Pin == registerStaffReqDto.Pin);
            if (userWithThatPin != null)
            {
                throw new AppException("Staff member with that Pin already exists");
            }
            
            var staffUser = new AppUser
            {
                FirstName = registerStaffReqDto.FirstName,
                LastName = registerStaffReqDto.LastName,
                Email = registerStaffReqDto.Email,
                UserName = registerStaffReqDto.PhoneNumber,
                PhoneNumber = registerStaffReqDto.PhoneNumber,
                Pin = registerStaffReqDto.Pin,
                BusinessId = currentUser.BusinessId
            };

            var createResult = await _userManager.CreateAsync(staffUser, "CantBeUsed!123");
            if (!createResult.Succeeded)
            {
                throw new AppException(createResult.ToString());
            }
        }

        public async Task UpdateAsync(UpdateStaffReqDto updateStaffReqReq)
        {
            var currentUser = await _authService.CurrentUser();
            var staffUser = await _userManager.FindByIdAsync(updateStaffReqReq.Id);

            if (staffUser == null || currentUser.BusinessId != staffUser.BusinessId)
            {
                throw new AppException("Staff not found");
            }

            staffUser.FirstName = updateStaffReqReq.FirstName;
            staffUser.LastName = updateStaffReqReq.LastName;
            staffUser.Email = updateStaffReqReq.Email;
            staffUser.PhoneNumber = updateStaffReqReq.PhoneNumber;
            staffUser.Pin = updateStaffReqReq.Pin;

            var updateResult = await _userManager.UpdateAsync(staffUser);
            if (!updateResult.Succeeded)
            {
                throw new AppException(updateResult.ToString());
            }
        }

        [AuthorizeRoles(UserRole.Owner)]
        public async Task DeleteAsync(DeleteStaffReqDto deleteStaffReq)
        {
            var currentUser = await _authService.CurrentUser();
            var staffUser = await _userManager.FindByIdAsync(deleteStaffReq.Id);

            if (staffUser == null || currentUser.BusinessId != staffUser.BusinessId)
            {
                throw new AppException("Invalid staff Id");
            }

            var deleteResult = await _userManager.DeleteAsync(staffUser);
            if (!deleteResult.Succeeded)
            {
                throw new AppException(deleteResult.ToString());
            }
        }

        [AuthorizeRoles(UserRole.Owner)]
        public async Task<ICollection<GetStaffResDto>> GetStaffsAsync()
        {
            var owner = await _authService.CurrentUser();
            var businessUsers = await _dbContext
                .Users
                .Where(u => u.BusinessId == owner.BusinessId)
                .ToListAsync();

            // Remove business owners
            var staffUsers = new List<GetStaffResDto>();
            foreach (var user in businessUsers)
            {
                if (!(await _userManager.IsInRoleAsync(user, UserRole.Owner)))
                {
                    staffUsers.Add(new GetStaffResDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Pin = user.Pin
                    });
                }
            }

            return staffUsers;
        }
    }
}