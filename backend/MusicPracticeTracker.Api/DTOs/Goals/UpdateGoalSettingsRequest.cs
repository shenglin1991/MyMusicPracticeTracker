using System.ComponentModel.DataAnnotations;

namespace MusicPracticeTracker.Api.DTOs.Goals;

public class UpdateGoalSettingsRequest
{
    [Range(0, 1440)]
    public int DailyTargetMinutes { get; set; }

    [Range(0, 10080)]
    public int WeeklyTargetMinutes { get; set; }
}
