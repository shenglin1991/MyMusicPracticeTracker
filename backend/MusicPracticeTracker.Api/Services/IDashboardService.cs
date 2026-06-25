using MusicPracticeTracker.Api.DTOs.Dashboard;

namespace MusicPracticeTracker.Api.Services;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}
