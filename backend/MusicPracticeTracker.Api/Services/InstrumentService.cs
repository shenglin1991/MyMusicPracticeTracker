using MusicPracticeTracker.Api.DTOs.Instruments;
using MusicPracticeTracker.Api.Entities;
using MusicPracticeTracker.Api.Repositories;

namespace MusicPracticeTracker.Api.Services;

public class InstrumentService(IInstrumentRepository instrumentRepository) : IInstrumentService
{
    public async Task<IReadOnlyCollection<InstrumentDto>> ListAsync()
    {
        var instruments = await instrumentRepository.ListAsync();
        return instruments.Select(Map).ToList();
    }

    public async Task<InstrumentDto> CreateAsync(CreateInstrumentRequest request)
    {
        var instrument = new Instrument
        {
            Name = request.Name.Trim(),
            Color = request.Color.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var createdInstrument = await instrumentRepository.AddAsync(instrument);
        return Map(createdInstrument);
    }

    public async Task<InstrumentDto?> UpdateAsync(Guid id, UpdateInstrumentRequest request)
    {
        var instrument = await instrumentRepository.GetByIdAsync(id);
        if (instrument is null)
        {
            return null;
        }

        instrument.Name = request.Name.Trim();
        instrument.Color = request.Color.Trim();

        var updatedInstrument = await instrumentRepository.UpdateAsync(instrument);
        return Map(updatedInstrument);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var instrument = await instrumentRepository.GetByIdAsync(id);
        if (instrument is null)
        {
            return false;
        }

        await instrumentRepository.DeleteAsync(instrument);
        return true;
    }

    private static InstrumentDto Map(Instrument instrument)
    {
        return new InstrumentDto(instrument.Id, instrument.Name, instrument.Color, instrument.CreatedAt);
    }
}
