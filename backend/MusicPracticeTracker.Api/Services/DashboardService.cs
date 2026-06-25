using MusicPracticeTracker.Api.DTOs.Dashboard;
using MusicPracticeTracker.Api.Repositories;

namespace MusicPracticeTracker.Api.Services;

public class DashboardService(
    IPracticeSessionRepository practiceSessionRepository,
    IGoalRepository goalRepository) : IDashboardService
{
    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var sessions = await practiceSessionRepository.ListAllAsync();
        var goal = await goalRepository.GetAsync();

        var now = DateTime.UtcNow;
        var todayStart = now.Date;
        var weekStart = StartOfWeek(now);
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var todayMinutes = sessions.Where(session => session.StartTime >= todayStart).Sum(session => session.DurationMinutes);
        var weekMinutes = sessions.Where(session => session.StartTime >= weekStart).Sum(session => session.DurationMinutes);
        var monthMinutes = sessions.Where(session => session.StartTime >= monthStart).Sum(session => session.DurationMinutes);
        var totalMinutes = sessions.Sum(session => session.DurationMinutes);

        var dailyTarget = goal?.DailyTargetMinutes ?? 30;
        var weeklyTarget = goal?.WeeklyTargetMinutes ?? 180;

        return new DashboardSummaryDto
        {
            TodayMinutes = todayMinutes,
            WeekMinutes = weekMinutes,
            MonthMinutes = monthMinutes,
            TotalMinutes = totalMinutes,
            BreakdownByInstrument = sessions
                .GroupBy(session => new { session.InstrumentId, session.Instrument.Name, session.Instrument.Color })
                .Select(group => new InstrumentBreakdownDto
                {
                    InstrumentId = group.Key.InstrumentId,
                    InstrumentName = group.Key.Name,
                    InstrumentColor = group.Key.Color,
                    TotalMinutes = group.Sum(item => item.DurationMinutes)
                })
                .OrderByDescending(item => item.TotalMinutes)
                .ToList(),
            Goals = new GoalProgressDto
            {
                DailyTargetMinutes = dailyTarget,
                WeeklyTargetMinutes = weeklyTarget,
                TodayProgressMinutes = todayMinutes,
                WeekProgressMinutes = weekMinutes,
                DailyCompletionPercentage = CalculateCompletion(todayMinutes, dailyTarget),
                WeeklyCompletionPercentage = CalculateCompletion(weekMinutes, weeklyTarget)
            }
        };
    }

    private static DateTime StartOfWeek(DateTime dateTime)
    {
        var difference = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;
        return dateTime.Date.AddDays(-difference);
    }

    private static decimal CalculateCompletion(int currentMinutes, int targetMinutes)
    {
        if (targetMinutes <= 0)
        {
            return 0;
        }

        return Math.Round((decimal)currentMinutes / targetMinutes * 100, 2);
    }
}
