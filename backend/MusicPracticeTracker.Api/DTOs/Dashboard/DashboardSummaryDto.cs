namespace MusicPracticeTracker.Api.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public int TodayMinutes { get; set; }
    public int WeekMinutes { get; set; }
    public int MonthMinutes { get; set; }
    public int TotalMinutes { get; set; }
    public IReadOnlyCollection<InstrumentBreakdownDto> BreakdownByInstrument { get; set; } = Array.Empty<InstrumentBreakdownDto>();
    public GoalProgressDto Goals { get; set; } = new();
}

public class InstrumentBreakdownDto
{
    public Guid InstrumentId { get; set; }
    public string InstrumentName { get; set; } = string.Empty;
    public string InstrumentColor { get; set; } = string.Empty;
    public int TotalMinutes { get; set; }
}

public class GoalProgressDto
{
    public int DailyTargetMinutes { get; set; }
    public int WeeklyTargetMinutes { get; set; }
    public int TodayProgressMinutes { get; set; }
    public int WeekProgressMinutes { get; set; }
    public decimal DailyCompletionPercentage { get; set; }
    public decimal WeeklyCompletionPercentage { get; set; }
}
