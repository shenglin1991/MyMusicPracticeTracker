using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public interface IGoalRepository
{
    Task<PracticeGoal?> GetAsync();
    Task<PracticeGoal> UpsertAsync(PracticeGoal goal);
}
