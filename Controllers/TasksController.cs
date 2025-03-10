using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskContext _context;

    public TasksController(TaskContext context)
    {
        _context = context;
    }

    // GET: api/tasks
    [HttpGet]
    public IActionResult GetTasks()
    {
        // throw new Exception("Test error");
        var tasks = _context.Tasks.ToList();
        return Ok(tasks);
    }

    // GET: api/tasks/5
    [HttpGet("{id}")]
    public IActionResult GetTask(int id)
    {
        var allTasks = _context.Tasks.ToList(); // Debug: Get all tasks
        // Console.WriteLine($"All tasks: {System.Text.Json.JsonSerializer.Serialize(allTasks)}");
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public IActionResult CreateTask([FromBody] TaskManagerApi.Models.Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    // PUT: api/tasks/5
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] TaskManagerApi.Models.Task task)
    {
        var existingTask = _context.Tasks.Find(id);
        if (existingTask == null) return NotFound();
        existingTask.Title = task.Title;
        existingTask.IsCompleted = task.IsCompleted;
        if (_context.SaveChanges() > 0)
        {
            return Ok(new { success = true, task = existingTask });
        }
        return BadRequest(new { success = false, message = "Failed to update task" });
    }

    // DELETE: api/tasks/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return NoContent();
    }
}