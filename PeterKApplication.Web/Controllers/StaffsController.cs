using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Auth;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthStaffResDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AuthStaff([FromBody] AuthStaffReqDto authStaffReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staffLoginRes = await _staffService.AuthenticateAsync(authStaffReq);
            if (staffLoginRes == null)
            {
                ModelState.AddModelError("Pin", "Invalid Pin for the given business");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            return Ok(staffLoginRes);
        }

        [HttpPost("register")]
        [AuthorizeRoles(UserRole.Owner)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffReqDto registerStaffReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _staffService.RegisterAsync(registerStaffReq);
                return Ok();
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPut]
        [AuthorizeRoles(UserRole.Owner)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateStaff([FromBody] UpdateStaffReqDto updateStaffReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _staffService.UpdateAsync(updateStaffReq);
                return Ok();
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpDelete]
        [AuthorizeRoles(UserRole.Owner)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteStaff([FromBody] DeleteStaffReqDto deleteStaffReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _staffService.DeleteAsync(deleteStaffReq);
                return Ok();
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpGet]
        [AuthorizeRoles(UserRole.Owner)]
        [ProducesResponseType(typeof(ICollection<GetStaffResDto>),200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStaffs()
        {
            var staffList = await _staffService.GetStaffsAsync();
            return Ok(staffList);
        }
    }
}