using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApi.Data;
using TaskBoardApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;
    public TasksController(AppDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll()
    {
        var tasks = await _db.Tasks
           .OrderBy(t => t.IsCompleted)
           .ThenByDescending(t => t.CreatedAt)
           .ToListAsync();
        return Ok(tasks);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null)
            return NotFound(new { Message = $"задача с id не найдена" });
        return Ok(task);
    }
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new { Message = "Поле обязательно для заполениния" });
        }
            
        task.Id = 0;
        task.IsCompleted = false;
        task.CreatedAt = DateTime.UtcNow;
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItem>> Update(int id, [FromBody] TaskItem updated)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null)
            return NotFound(new { Message = $"задача с id не найжена" });
        if (string.IsNullOrWhiteSpace(updated.Title))
            return BadRequest(new { Message = "Поле обязательно для заполениния" });

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.IsCompleted = updated.IsCompleted;
        await _db.SaveChangesAsync();
        return Ok(task);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null)
            return NotFound(new { Message = $"задача с id не найжена" });
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return NoContent();
    }

}