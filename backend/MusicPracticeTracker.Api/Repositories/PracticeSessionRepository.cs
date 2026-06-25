using Microsoft.EntityFrameworkCore;
using MusicPracticeTracker.Api.Data;
using MusicPracticeTracker.Api.Entities;

namespace MusicPracticeTracker.Api.Repositories;

public class PracticeSessionRepository(ApplicationDbContext dbContext) : IPracticeSessionRepository
{
    public async Task<IReadOnlyCollection<PracticeSession>> ListAsync(Guid? instrumentId, DateTime? from, DateTime? to)
    {
        var query = dbContext.PracticeSessions
            .AsNoTracking()
            .Include(session => session.Instrument)
            .AsQueryable();

        if (instrumentId.HasValue)
        {
            query = query.Where(session => session.InstrumentId == instrumentId.Value);
        }

        if (from.HasValue)
        {
            query = query.Where(session => session.StartTime >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(session => session.EndTime <= to.Value);
        }

        return await query
            .OrderByDescending(session => session.StartTime)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<PracticeSession>> ListAllAsync()
    {
        return await dbContext.PracticeSessions
            .AsNoTracking()
            .Include(session => session.Instrument)
            .ToListAsync();
    }

    public async Task<PracticeSession?> GetByIdAsync(Guid id)
    {
        return await dbContext.PracticeSessions
            .Include(session => session.Instrument)
            .FirstOrDefaultAsync(session => session.Id == id);
    }

    public async Task<PracticeSession> AddAsync(PracticeSession session)
    {
        dbContext.PracticeSessions.Add(session);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(session).Reference(item => item.Instrument).LoadAsync();
        return session;
    }

    public async Task<PracticeSession> UpdateAsync(PracticeSession session)
    {
        dbContext.PracticeSessions.Update(session);
        await dbContext.SaveChangesAsync();
        await dbContext.Entry(session).Reference(item => item.Instrument).LoadAsync();
        return session;
    }

    public async Task DeleteAsync(PracticeSession session)
    {
        dbContext.PracticeSessions.Remove(session);
        await dbContext.SaveChangesAsync();
    }
}
