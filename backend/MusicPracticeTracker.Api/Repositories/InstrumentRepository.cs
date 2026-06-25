using Microsoft.EntityFrameworkCore;
using MusicPracticeTracker.Api.Data;
using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public class InstrumentRepository(ApplicationDbContext dbContext) : IInstrumentRepository
{
    public async Task<IReadOnlyCollection<Instrument>> ListAsync()
    {
        return await dbContext.Instruments
            .AsNoTracking()
            .OrderBy(instrument => instrument.Name)
            .ToListAsync();
    }

    public async Task<Instrument?> GetByIdAsync(Guid id)
    {
        return await dbContext.Instruments.FirstOrDefaultAsync(instrument => instrument.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await dbContext.Instruments.AnyAsync(instrument => instrument.Id == id);
    }

    public async Task<Instrument> AddAsync(Instrument instrument)
    {
        dbContext.Instruments.Add(instrument);
        await dbContext.SaveChangesAsync();
        return instrument;
    }

    public async Task<Instrument> UpdateAsync(Instrument instrument)
    {
        dbContext.Instruments.Update(instrument);
        await dbContext.SaveChangesAsync();
        return instrument;
    }

    public async Task DeleteAsync(Instrument instrument)
    {
        dbContext.Instruments.Remove(instrument);
        await dbContext.SaveChangesAsync();
    }
}
