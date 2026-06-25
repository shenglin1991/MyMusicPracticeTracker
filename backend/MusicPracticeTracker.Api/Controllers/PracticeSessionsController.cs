using Microsoft.AspNetCore.Mvc;
using MusicPracticeTracker.Api.DTOs.PracticeSessions;
using MusicPracticeTracker.Api.Services;

namespace MusicPracticeTracker.Api.Controllers;

[ApiController]
[Route("api/practice-sessions")]
public class PracticeSessionsController(IPracticeSessionService practiceSessionService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<PracticeSessionDto>>> ListAsync(
        [FromQuery] Guid? instrumentId,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        var sessions = await practiceSessionService.ListAsync(instrumentId, from, to);
        return Ok(sessions);
    }

    [HttpPost]
    public async Task<ActionResult<PracticeSessionDto>> CreateAsync([FromBody] CreatePracticeSessionRequest request)
    {
        try
        {
            var createdSession = await practiceSessionService.CreateAsync(request);
            return Created($"/api/practice-sessions/{createdSession.Id}", createdSession);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PracticeSessionDto>> UpdateAsync(Guid id, [FromBody] UpdatePracticeSessionRequest request)
    {
        try
        {
            var updatedSession = await practiceSessionService.UpdateAsync(id, request);
            return updatedSession is null ? NotFound() : Ok(updatedSession);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await practiceSessionService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
