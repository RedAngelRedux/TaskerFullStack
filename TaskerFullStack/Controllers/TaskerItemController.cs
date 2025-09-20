using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskerFullStack.Client.Models;
using TaskerFullStack.Data;
using TaskerFullStack.Models;

namespace TaskerFullStack.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskerItemController(
    ApplicationDbContext db,
    UserManager<ApplicationUser> userManager) 
    : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _db = db;

    [HttpPost]
    public async Task<ActionResult<TaskerItem>> PostDbTaskerItem([FromBody] TaskerItem takserItem)
    {
        DbTaskerItem dbTaskerItem = new()
        {
            Id = takserItem.Id,
            Name = takserItem.Name,
            IsComplete = takserItem.IsComplete,
            UserId = _userManager.GetUserId(User)
        };

        _db.TaskerItems.Add(dbTaskerItem);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
