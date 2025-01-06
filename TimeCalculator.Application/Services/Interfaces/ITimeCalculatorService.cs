using TimeCalculator.Application.DTOs;

namespace TimeCalculator.Application.Services.Interfaces;

public interface ITimeCalculatorService
{
    Task<TimeEntryDto> AddTimeEntryAsync(TimeEntryDto timeEntryDto);
    Task<TimeEntryDto?> GetTimeEntryByDateAsync(DateTime date);
    Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForPeriodAsync(DateTime startDate, DateTime endDate);
    Task<TimeSpan> GetTotalWorkTimeForPeriodAsync(DateTime startDate, DateTime endDate);
    Task UpdateTimeEntryAsync(TimeEntryDto timeEntryDto);
    Task DeleteTimeEntryAsync(int id);
}