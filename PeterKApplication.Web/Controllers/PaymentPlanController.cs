using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;
using Microsoft.AspNetCore.Authorization;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentPlanController : ControllerBase
    {
        private readonly IPaymentPlanService _paymentPlanService;

        public PaymentPlanController(IPaymentPlanService paymentPlanService)
        {
            _paymentPlanService = paymentPlanService;
        }
        
        [HttpGet]
        [AuthorizeRoles(UserRole.Administrator, UserRole.Owner)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public IActionResult GetPaymentPlans()
        {
            var paymentPlans = _paymentPlanService.GetPaymentPlans();
            return Ok(paymentPlans);
        }

        [HttpPost]
        [AuthorizeRoles(UserRole.Administrator)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddPaymentPlan([FromBody] PaymentPlanDto paymentPlan)
        {
            await _paymentPlanService.AddPaymentPlan(paymentPlan);
            return Ok();
        }
        
        [HttpPut]
        [AuthorizeRoles(UserRole.Administrator)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePaymentPlan([FromBody] PaymentPlanDto paymentPlan)
        {
            try
            {
                await _paymentPlanService.UpdatePaymentPlan(paymentPlan);
                return Ok();
            }
            catch (AppException e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
    }
}