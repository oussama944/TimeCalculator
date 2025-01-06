using Microsoft.EntityFrameworkCore;
using TimeCalculator.Domain.Entities;
using TimeCalculator.Domain.Interfaces;
using TimeCalculator.Infrastructure.Data;

namespace TimeCalculator.Infrastructure.Repositories;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly ApplicationDbContext _context;

    public TimeEntryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TimeEntry> GetByIdAsync(int id)
    {
        var entry = await _context.TimeEntries.FindAsync(id);
        return entry ?? throw new KeyNotFoundException($"TimeEntry with ID {id} was not found.");
    }

    public async Task<IEnumerable<TimeEntry>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        return await _context.TimeEntries
            .Where(t => t.Date >= start && t.Date <= end)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<TimeEntry> GetByDateAsync(DateTime date)
        => await _context.TimeEntries
            .FirstOrDefaultAsync(t => t.Date.Date == date.Date)
            ?? throw new KeyNotFoundException($"Aucune entrée trouvée pour la date {date:d}");

    public async Task<int> AddAsync(TimeEntry timeEntry)
    {
        await _context.TimeEntries.AddAsync(timeEntry);
        await _context.SaveChangesAsync();
        return timeEntry.Id;
    }

    public async Task UpdateAsync(TimeEntry timeEntry)
    {
        _context.TimeEntries.Update(timeEntry);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var timeEntry = await _context.TimeEntries.FindAsync(id);
        if (timeEntry != null)
        {
            _context.TimeEntries.Remove(timeEntry);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(DateTime date)
    {
        return await _context.TimeEntries
            .AnyAsync(t => t.Date.Date == date.Date);
    }

    public async Task<TimeSpan> GetTotalWorkTimeForPeriodAsync(DateTime start, DateTime end)
    {
        var entries = await GetByDateRangeAsync(start, end);
        return entries.Aggregate(TimeSpan.Zero,
            (total, entry) => total + entry.CalculateTotalWorkDuration());
    }
}