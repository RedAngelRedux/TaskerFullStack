using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using TaskerFullStack.Client.Models;
using TaskerFullStack.Data;
using TaskerFullStack.Migrations;
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

    // post:  api/TaskerItems
    [HttpPost]
    public async Task<ActionResult<TaskerItem>> PostDbTaskerItem([FromBody] TaskerItem takserItem)
    {
        DbTaskerItem dbTaskerItem = new()
        {
            Id = takserItem.Id,
            Name = takserItem.Name,
            IsComplete = takserItem.IsComplete,
            UserId = CurrentUserId
        };

        _db.TaskerItems.Add(dbTaskerItem);
        await _db.SaveChangesAsync();

        return Ok();
    }

    // get:  api/TaskerItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskerItem>>> GetDbTaskerItems()
    {
        List<DbTaskerItem> items = await _db.TaskerItems.Where(t => t.UserId == CurrentUserId).ToListAsync();
        return items;
    }

    private string CurrentUserId => _userManager.GetUserId(User) ?? string.Empty;
}
