using Microsoft.AspNetCore.Mvc;
using MusicPracticeTracker.Api.DTOs.Dashboard;
using MusicPracticeTracker.Api.Services;

namespace MusicPracticeTracker.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardSummaryDto>> GetAsync()
    {
        var summary = await dashboardService.GetSummaryAsync();
        return Ok(summary);
    }
}
