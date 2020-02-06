using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Auth;
using RestSharp;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(UserRole.Owner)]
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController: ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IAuthService _authService;

        public OwnersController(IOwnerService ownerService, IAuthService authService)
        {
            _ownerService = ownerService;
            _authService = authService;
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterOwnerResDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] RegisterOwnerReqDto registerOwnerReq)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registerResponse = await _ownerService.RegisterAsync(registerOwnerReq);
                return Ok(registerResponse);
            }
            catch (AppException e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
        
        [AllowAnonymous]
        [HttpPost("verifyPhoneNumber")]
        [ProducesResponseType(typeof(VerifyPhoneNumberResDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> VerifyPhoneNumber([FromBody] VerifyPhoneNumberReqDto verifyPhoneNumberReq)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var verifyPhoneResponse = await _ownerService.VerifyPhoneNumber(verifyPhoneNumberReq);
                return Ok(verifyPhoneResponse);
            }
            catch (AppException e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
        
        [AllowAnonymous]
        [HttpPost("sendConfirmationCode")]
        [ProducesResponseType(typeof(SmsConfirmationResDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendConfirmationCode([FromBody] SmsConfirmationReqDto smsConfirmationReq)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sendSmsResponse = await _ownerService.SendSmsConfirmationAsync(smsConfirmationReq);
                return Ok(sendSmsResponse);
            }
            catch (AppException e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthOwnerResDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AuthOwner([FromBody] AuthOwnerReqDto authOwnerReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var authenticateRes = await _ownerService.AuthenticateAsync(authOwnerReq);
                return new JsonResult(authenticateRes);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpGet("refresh")]
        [ProducesResponseType(typeof(AuthOwnerResDto), 200)]
        public async Task<IActionResult> RefreshOwner()
        {
            return new JsonResult(new AuthOwnerResDto
            {
                Token = await _authService.RefreshToken()
            });
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateOwner([FromBody] UpdateOwnerReqDto updateOwnerReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ownerService.UpdateAsync(updateOwnerReq);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            return Ok();
        }

        [HttpPost("pin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePin([FromBody] UpdateOwnerPinDto updateOwnerPin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ownerService.UpdatePin(updateOwnerPin);
                return Ok();
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpGet("agent")]
        [ProducesResponseType(typeof(AgentDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAgent()
        {
            try
            {
                var agent = await _ownerService.GetAgent();
                return new JsonResult(agent);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPost("agent")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateAgent([FromBody] AgentDto agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                await _ownerService.UpdateAgent(agent);
                return Ok();
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
    }
}