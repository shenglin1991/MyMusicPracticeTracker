using MusicPracticeTracker.Api.DTOs.Goals;
using MusicPracticeTracker.Api.Entities;
using MusicPracticeTracker.Api.Repositories;

namespace MusicPracticeTracker.Api.Services;

public class GoalService(IGoalRepository goalRepository) : IGoalService
{
    public async Task<GoalSettingsDto> GetAsync()
    {
        var goal = await goalRepository.GetAsync() ?? GetDefaultGoal();
        return Map(goal);
    }

    public async Task<GoalSettingsDto> UpdateAsync(UpdateGoalSettingsRequest request)
    {
        var goal = new PracticeGoal
        {
            Id = 1,
            DailyTargetMinutes = request.DailyTargetMinutes,
            WeeklyTargetMinutes = request.WeeklyTargetMinutes,
            CreatedAt = DateTime.UtcNow
        };

        var updatedGoal = await goalRepository.UpsertAsync(goal);
        return Map(updatedGoal);
    }

    private static GoalSettingsDto Map(PracticeGoal goal)
    {
        return new GoalSettingsDto
        {
            DailyTargetMinutes = goal.DailyTargetMinutes,
            WeeklyTargetMinutes = goal.WeeklyTargetMinutes
        };
    }

    private static PracticeGoal GetDefaultGoal()
    {
        return new PracticeGoal
        {
            Id = 1,
            DailyTargetMinutes = 30,
            WeeklyTargetMinutes = 180,
            CreatedAt = DateTime.UtcNow
        };
    }
}
