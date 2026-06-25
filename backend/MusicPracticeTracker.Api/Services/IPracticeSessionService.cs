using MusicPracticeTracker.Api.DTOs.PracticeSessions;

namespace MusicPracticeTracker.Api.Services;

public interface IPracticeSessionService
{
    Task<IReadOnlyCollection<PracticeSessionDto>> ListAsync(Guid? instrumentId, DateTime? from, DateTime? to);
    Task<PracticeSessionDto> CreateAsync(CreatePracticeSessionRequest request);
    Task<PracticeSessionDto?> UpdateAsync(Guid id, UpdatePracticeSessionRequest request);
    Task<bool> DeleteAsync(Guid id);
}
