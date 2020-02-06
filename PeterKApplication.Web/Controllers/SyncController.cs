using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PeterKApplication.Shared.Dtos;
using PeterKApplication.Web.Exceptions;
using PeterKApplication.Web.Services;

namespace PeterKApplication.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SyncDetailsDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SyncInfo([FromBody] SyncDetailsDto syncDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var retSyncDetails = await _syncService.SyncDetails(syncDetails);
                return Ok(retSyncDetails);
            }
            catch (AppException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(SyncDto), 200)]
        [ProducesResponseType(typeof(ApiExceptionDto), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Sync([FromBody] SyncDto sync)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resSync = await _syncService.Sync(sync);

            return Ok(resSync);
        }
    }
}