using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public interface IInstrumentRepository
{
    Task<IReadOnlyCollection<Instrument>> ListAsync();
    Task<Instrument?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<Instrument> AddAsync(Instrument instrument);
    Task<Instrument> UpdateAsync(Instrument instrument);
    Task DeleteAsync(Instrument instrument);
}
