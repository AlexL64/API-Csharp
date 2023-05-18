using API_Csharp.Models;
using API_Csharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TasksService _TasksService;

    public TasksController(TasksService TasksService) =>
        _TasksService = TasksService;

    [HttpGet]
    public async Task<List<Models.Task>> Get() =>
        await _TasksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Models.Task>> Get(string id)
    {
        var task = await _TasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Task newTask)
    {
        await _TasksService.CreateAsync(newTask);

        return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Models.Task updatedTask)
    {
        var task = await _TasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        updatedTask.Id = task.Id;

        await _TasksService.UpdateAsync(id, updatedTask);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _TasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _TasksService.RemoveAsync(id);

        return NoContent();
    }
}