using API_Csharp.Models;
using API_Csharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API_Csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly CommentsService _CommentsService;

    public CommentsController(CommentsService CommentsService) =>
        _CommentsService = CommentsService;

    [HttpGet]
    public async Task<List<Comment>> Get() =>
        await _CommentsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Comment>> Get(string id)
    {
        var comment = await _CommentsService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpGet("Task/{taskId:length(24)}")]
    public async Task<List<Comment>> GetForTask(string taskId) =>
        await _CommentsService.GetForTaskAsync(taskId);

    [HttpPost]
    public async Task<IActionResult> Post(Comment newComment)
    {
        await _CommentsService.CreateAsync(newComment);

        return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Comment updatedComment)
    {
        var comment = await _CommentsService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        updatedComment.Id = comment.Id;

        await _CommentsService.UpdateAsync(id, updatedComment);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var comment = await _CommentsService.GetAsync(id);

        if (comment is null)
        {
            return NotFound();
        }

        await _CommentsService.RemoveAsync(id);

        return NoContent();
    }


    [HttpDelete("Task/{taskId:length(24)}")]
    public async Task<IActionResult> DeleteForTask(string taskId)
    {
        var comment = await _CommentsService.GetForTaskAsync(taskId);

        if (comment is null)
        {
            return NotFound();
        }

        await _CommentsService.RemoveForTaskAsync(taskId);

        return NoContent();
    }
}

