using MusicPracticeTracker.Api.DTOs.Goals;

namespace MusicPracticeTracker.Api.Services;

public interface IGoalService
{
    Task<GoalSettingsDto> GetAsync();
    Task<GoalSettingsDto> UpdateAsync(UpdateGoalSettingsRequest request);
}
