using TimeCalculator.Domain.Entities;

namespace TimeCalculator.Domain.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> GetByIdAsync(int id);
        Task<IEnumerable<TimeEntry>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<TimeEntry> GetByDateAsync(DateTime date);
        Task<int> AddAsync(TimeEntry timeEntry);
        Task UpdateAsync(TimeEntry timeEntry);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(DateTime date);
        Task<TimeSpan> GetTotalWorkTimeForPeriodAsync(DateTime start, DateTime end);
    }
}
