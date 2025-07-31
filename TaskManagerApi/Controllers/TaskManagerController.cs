using TaskManagerApi.Models;
using TaskManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskManagerController : ControllerBase
{
    private readonly TasksService tasksService;

    public TaskManagerController(TasksService _tasksService) =>
        tasksService = _tasksService;

    [HttpGet]
    public async Task<List<TodoTask>> Get()
    {
        var dbTasks = await tasksService.GetAsync();
        return dbTasks.Select(t => TodoTask.fromDbTask(t)).ToList();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<DbTask>> Get(string id)
    {
        var task = await tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(DbTask newTask)
    {
        await tasksService.CreateAsync(newTask);

        return CreatedAtAction(nameof(Get), new { id = newTask.id }, newTask);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, DbTask updatedTask)
    {
        var task = await tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        updatedTask.dueDate = DateTime.Now;

        updatedTask.id = task.id;

        await tasksService.UpdateAsync(id, updatedTask);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await tasksService.RemoveAsync(id);

        return NoContent();
    }
}