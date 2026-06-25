using MusicPracticeTracker.Api.DTOs.PracticeSessions;
using MusicPracticeTracker.Api.Entities;
using MusicPracticeTracker.Api.Repositories;

namespace MusicPracticeTracker.Api.Services;

public class PracticeSessionService(
    IPracticeSessionRepository practiceSessionRepository,
    IInstrumentRepository instrumentRepository) : IPracticeSessionService
{
    public async Task<IReadOnlyCollection<PracticeSessionDto>> ListAsync(Guid? instrumentId, DateTime? from, DateTime? to)
    {
        var sessions = await practiceSessionRepository.ListAsync(instrumentId, from, to);
        return sessions.Select(Map).ToList();
    }

    public async Task<PracticeSessionDto> CreateAsync(CreatePracticeSessionRequest request)
    {
        await ValidateInstrumentAsync(request.InstrumentId);
        var (startTime, endTime, durationMinutes) = NormalizeTiming(request.StartTime, request.EndTime);

        var session = new PracticeSession
        {
            InstrumentId = request.InstrumentId,
            StartTime = startTime,
            EndTime = endTime,
            DurationMinutes = durationMinutes,
            Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var createdSession = await practiceSessionRepository.AddAsync(session);
        return Map(createdSession);
    }

    public async Task<PracticeSessionDto?> UpdateAsync(Guid id, UpdatePracticeSessionRequest request)
    {
        await ValidateInstrumentAsync(request.InstrumentId);
        var session = await practiceSessionRepository.GetByIdAsync(id);
        if (session is null)
        {
            return null;
        }

        var (startTime, endTime, durationMinutes) = NormalizeTiming(request.StartTime, request.EndTime);
        session.InstrumentId = request.InstrumentId;
        session.StartTime = startTime;
        session.EndTime = endTime;
        session.DurationMinutes = durationMinutes;
        session.Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim();

        var updatedSession = await practiceSessionRepository.UpdateAsync(session);
        return Map(updatedSession);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var session = await practiceSessionRepository.GetByIdAsync(id);
        if (session is null)
        {
            return false;
        }

        await practiceSessionRepository.DeleteAsync(session);
        return true;
    }

    private async Task ValidateInstrumentAsync(Guid instrumentId)
    {
        if (!await instrumentRepository.ExistsAsync(instrumentId))
        {
            throw new InvalidOperationException("The selected instrument does not exist.");
        }
    }

    private static (DateTime StartTime, DateTime EndTime, int DurationMinutes) NormalizeTiming(DateTime startTime, DateTime endTime)
    {
        var normalizedStartTime = startTime.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(startTime, DateTimeKind.Utc) : startTime.ToUniversalTime();
        var normalizedEndTime = endTime.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(endTime, DateTimeKind.Utc) : endTime.ToUniversalTime();

        if (normalizedEndTime <= normalizedStartTime)
        {
            throw new InvalidOperationException("End time must be later than start time.");
        }

        var durationMinutes = Math.Max(1, (int)Math.Round((normalizedEndTime - normalizedStartTime).TotalMinutes, MidpointRounding.AwayFromZero));
        return (normalizedStartTime, normalizedEndTime, durationMinutes);
    }

    private static PracticeSessionDto Map(PracticeSession session)
    {
        return new PracticeSessionDto(
            session.Id,
            session.InstrumentId,
            session.Instrument.Name,
            session.Instrument.Color,
            session.StartTime,
            session.EndTime,
            session.DurationMinutes,
            session.Notes,
            session.CreatedAt);
    }
}
