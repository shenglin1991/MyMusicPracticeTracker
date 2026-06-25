using Microsoft.EntityFrameworkCore;
using MusicPracticeTracker.Api.Data;
using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public class GoalRepository(ApplicationDbContext dbContext) : IGoalRepository
{
    public async Task<PracticeGoal?> GetAsync()
    {
        return await dbContext.PracticeGoals.FirstOrDefaultAsync(goal => goal.Id == 1);
    }

    public async Task<PracticeGoal> UpsertAsync(PracticeGoal goal)
    {
        var existingGoal = await dbContext.PracticeGoals.FirstOrDefaultAsync(item => item.Id == goal.Id);
        if (existingGoal is null)
        {
            dbContext.PracticeGoals.Add(goal);
        }
        else
        {
            existingGoal.DailyTargetMinutes = goal.DailyTargetMinutes;
            existingGoal.WeeklyTargetMinutes = goal.WeeklyTargetMinutes;
        }

        await dbContext.SaveChangesAsync();
        return existingGoal ?? goal;
    }
}
