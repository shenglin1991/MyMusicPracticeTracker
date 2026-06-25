using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public interface IPracticeSessionRepository
{
    Task<IReadOnlyCollection<PracticeSession>> ListAsync(Guid? instrumentId, DateTime? from, DateTime? to);
    Task<IReadOnlyCollection<PracticeSession>> ListAllAsync();
    Task<PracticeSession?> GetByIdAsync(Guid id);
    Task<PracticeSession> AddAsync(PracticeSession session);
    Task<PracticeSession> UpdateAsync(PracticeSession session);
    Task DeleteAsync(PracticeSession session);
}
