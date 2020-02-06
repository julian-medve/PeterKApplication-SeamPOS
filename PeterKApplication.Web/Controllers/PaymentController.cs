using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;
using Microsoft.AspNetCore.Authorization;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using PeterKApplication.Web.Auth;
using Sentry;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IVisaService _visaService;
        private readonly IMPesaService _mPesaService;

        public PaymentController(IVisaService visaService, IMPesaService mPesaService)
        {
            _visaService = visaService;
            _mPesaService = mPesaService;
        }
        
        [HttpPost("visa")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> VisaPayment([FromBody] VisaPaymentReqDto visaPaymentReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var visaPaymentRes = await _visaService.MerchantPushPayment(visaPaymentReq);
                return Ok(visaPaymentRes);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
        
        [HttpPost("mPesa")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> MPesaPayment([FromBody] MPesaPaymentReqDto mPesaPaymentReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mPesaPaymentRes = await _mPesaService.MPesaOrderPayment(mPesaPaymentReq);
                return Ok(mPesaPaymentRes);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [AllowAnonymous]
        [HttpPost("validation")]
        public async Task<IActionResult> MPesaValidationPostAsync([FromBody] MPesaTransaction mPesaTransaction)
        {
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                mPesaTransaction.Json = stream.ReadToEnd();
            }

            await _mPesaService.ValidateTransaction(mPesaTransaction);
            
            const string response = "{\"ResultCode\": 0, \"ResultDesc\": \"Accepted\"}";
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("confirmation")]
        public async Task<IActionResult> MPesaConfirmationPostAsync()
        {
            
            

            const string response = "{\"C2BPaymentConfirmationResult\": \"Success\"}";
            
            return Ok(response);
        }
        
        [AllowAnonymous]
        [HttpPost("validation1")]
        public async Task<IActionResult> MPesaValidation2PostAsync()
        {
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                string body = stream.ReadToEnd();
                Console.WriteLine(body);
                SentrySdk.CaptureMessage(body);
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("confirmation1")]
        public async Task<IActionResult> MPesaConfirmation2PostAsync()
        {
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                string body = stream.ReadToEnd();
                Console.WriteLine(body);
                SentrySdk.CaptureMessage(body);
            }
            return Ok();
        }
    }
}