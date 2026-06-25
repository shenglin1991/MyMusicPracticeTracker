namespace MusicPracticeTracker.Api.DTOs.PracticeSessions;

public record PracticeSessionDto(
    Guid Id,
    Guid InstrumentId,
    string InstrumentName,
    string InstrumentColor,
    DateTime StartTime,
    DateTime EndTime,
    int DurationMinutes,
    string? Notes,
    DateTime CreatedAt);
