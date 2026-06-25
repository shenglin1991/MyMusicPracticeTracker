using MusicPracticeTracker.Api.DTOs.Instruments;

namespace MusicPracticeTracker.Api.Services;

public interface IInstrumentService
{
    Task<IReadOnlyCollection<InstrumentDto>> ListAsync();
    Task<InstrumentDto> CreateAsync(CreateInstrumentRequest request);
    Task<InstrumentDto?> UpdateAsync(Guid id, UpdateInstrumentRequest request);
    Task<bool> DeleteAsync(Guid id);
}
