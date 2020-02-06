using System;
using System.Collections.Generic;
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
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;

        public KnowledgeBaseController(IKnowledgeBaseService knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }

        [HttpGet]
        [AuthorizeRoles(UserRole.Administrator, UserRole.Owner)]
        [ProducesResponseType(typeof(ICollection<KnowledgeBaseDto>), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public IActionResult GetKnowledgeBase()
        {
            var kbList = _knowledgeBaseService.GetKnowledgeBase();
            return Ok(kbList);
        }

        [HttpPost]
        [AuthorizeRoles(UserRole.Administrator)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddKnowledgeBase([FromBody] KnowledgeBaseDto knowledgeBase)
        {
            await _knowledgeBaseService.AddKnowledgeBase(knowledgeBase);
            return Ok();
        }
        
        [HttpPut]
        [AuthorizeRoles(UserRole.Administrator)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateKnowledgeBase([FromBody] KnowledgeBaseDto knowledgeBase)
        {
            try
            {
                await _knowledgeBaseService.UpdateKnowledgeBase(knowledgeBase);
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