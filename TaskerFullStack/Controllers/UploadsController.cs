using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using TaskerFullStack.Data;
using TaskerFullStack.Models;

namespace TaskerFullStack.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class UploadsController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet("{id:Guid}")]
        [OutputCache(VaryByRouteValueNames = ["id"], Duration = 60 * 60 * 24)]
        public async Task<IActionResult> GetImage(Guid id)
        {
            ImageUpload? image = await context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image is null || (image.Data is null || image.Type is null))
            {
                return NotFound();
            }
            else
            {
                return File(image.Data, image.Type);
            }
        }
    }
}
