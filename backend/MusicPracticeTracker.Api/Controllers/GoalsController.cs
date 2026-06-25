using Microsoft.AspNetCore.Mvc;
using MusicPracticeTracker.Api.DTOs.Goals;
using MusicPracticeTracker.Api.Services;

namespace MusicPracticeTracker.Api.Controllers;

[ApiController]
[Route("api/goals")]
public class GoalsController(IGoalService goalService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GoalSettingsDto>> GetAsync()
    {
        var goalSettings = await goalService.GetAsync();
        return Ok(goalSettings);
    }

    [HttpPut]
    public async Task<ActionResult<GoalSettingsDto>> UpdateAsync([FromBody] UpdateGoalSettingsRequest request)
    {
        var updatedGoalSettings = await goalService.UpdateAsync(request);
        return Ok(updatedGoalSettings);
    }
}
