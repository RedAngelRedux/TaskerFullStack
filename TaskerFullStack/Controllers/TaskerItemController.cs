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

        // This is REST compliant
        return CreatedAtAction("GetTaskerItem", new {id = dbTaskerItem.Id, dbTaskerItem});
    }

    // get:  api/TaskerItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskerItem>>> GetTaskerItems()
    {
        List<DbTaskerItem> items = await _db.TaskerItems.Where(t => t.UserId == CurrentUserId).ToListAsync();
        return items;
    }

    // get:  api/TaskerItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskerItem>> GetTaskerItem([FromRoute] Guid id)
    {
        DbTaskerItem? dbTaskerItem = await _db.TaskerItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId) ;
        if(dbTaskerItem == null)
        {
            return NotFound();
        }
        else
        {
            return dbTaskerItem;
        }
    }

    // put:  api/TaskerItems/5
    [HttpPut("{id}")]
    public async Task<ActionResult> PutDbTaskerItem([FromRoute] Guid id, [FromBody] TaskerItem taskerItem)
    {
        // Validate id; must match taskerItem & exists in Db
        if (id != taskerItem.Id) return BadRequest();
        DbTaskerItem? dbTaskerItem = await _db.TaskerItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId);
        if(dbTaskerItem == null) return NotFound();

        // Update the Record
        dbTaskerItem.Name = taskerItem.Name;
        dbTaskerItem.IsComplete = taskerItem.IsComplete;

        _db.Update(dbTaskerItem);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // delete:  api/TaskerItems/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskerItem([FromRoute] Guid id)
    {
        // Validate id; exists in Db
        DbTaskerItem? dbTaskerItem = await _db.TaskerItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == CurrentUserId);
        
        if (dbTaskerItem == null) {
            return NotFound();
        }
        else
        {
            _db.TaskerItems.Remove(dbTaskerItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }


    private string CurrentUserId => _userManager.GetUserId(User) ?? string.Empty;
}
