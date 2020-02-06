using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Auth;

namespace PeterKApplication.Web.Controllers
{
    [AuthorizeRoles(UserRole.Administrator)]
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ImagesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetImageAsync(Guid id)
        {
            var imgBytes = await _dbContext.ImageModels.FirstOrDefaultAsync(i => i.Id == id);
            if (imgBytes == null)
            {
                return BadRequest();
            }

            return File(imgBytes.ImageData, "Image/jpeg", id + ".jpg");
        }
    }
}