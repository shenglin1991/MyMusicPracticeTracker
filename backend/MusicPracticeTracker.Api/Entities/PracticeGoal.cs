namespace MusicPracticeTracker.Api.Entities;

public class PracticeGoal
{
    public int Id { get; set; }
    public int DailyTargetMinutes { get; set; }
    public int WeeklyTargetMinutes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
