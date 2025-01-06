using TimeCalculator.Application.DTOs;
using TimeCalculator.Application.Services.Interfaces;
using TimeCalculator.Domain.Entities;
using TimeCalculator.Domain.Interfaces;

namespace TimeCalculator.Application.Services;

public class TimeCalculatorService : ITimeCalculatorService
{
    private readonly ITimeEntryRepository _repository;

    public TimeCalculatorService(ITimeEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<TimeEntryDto> AddTimeEntryAsync(TimeEntryDto dto)
    {
        var timeEntry = new TimeEntry
        {
            Date = dto.Date,
            MorningStart = dto.MorningStart,
            MorningEnd = dto.MorningEnd,
            AfternoonStart = dto.AfternoonStart,
            AfternoonEnd = dto.AfternoonEnd,
            MinimumLunchBreak = dto.MinimumLunchBreak
        };

        if (!timeEntry.IsLunchBreakValid())
        {
            throw new InvalidOperationException("La pause déjeuner est inférieure au minimum requis");
        }

        var id = await _repository.AddAsync(timeEntry);
        dto.Id = id;
        return dto;
    }

    public async Task<TimeEntryDto?> GetTimeEntryByDateAsync(DateTime date)
    {
        var entry = await _repository.GetByDateAsync(date);
        if (entry == null) return null;

        return MapToDto(entry);
    }

    public async Task<IEnumerable<TimeEntryDto>> GetTimeEntriesForPeriodAsync(
        DateTime startDate, DateTime endDate)
    {
        var entries = await _repository.GetByDateRangeAsync(startDate, endDate);
        return entries.Select(MapToDto);
    }

    public async Task<TimeSpan> GetTotalWorkTimeForPeriodAsync(
        DateTime startDate, DateTime endDate)
    {
        return await _repository.GetTotalWorkTimeForPeriodAsync(startDate, endDate);
    }

    public async Task UpdateTimeEntryAsync(TimeEntryDto dto)
    {
        var timeEntry = new TimeEntry
        {
            Id = dto.Id,
            Date = dto.Date,
            MorningStart = dto.MorningStart,
            MorningEnd = dto.MorningEnd,
            AfternoonStart = dto.AfternoonStart,
            AfternoonEnd = dto.AfternoonEnd,
            MinimumLunchBreak = dto.MinimumLunchBreak
        };

        if (!timeEntry.IsLunchBreakValid())
        {
            throw new InvalidOperationException("La pause déjeuner est inférieure au minimum requis");
        }

        await _repository.UpdateAsync(timeEntry);
    }

    public async Task DeleteTimeEntryAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    private static TimeEntryDto MapToDto(TimeEntry entry)
    {
        return new TimeEntryDto
        {
            Id = entry.Id,
            Date = entry.Date,
            MorningStart = entry.MorningStart,
            MorningEnd = entry.MorningEnd,
            AfternoonStart = entry.AfternoonStart,
            AfternoonEnd = entry.AfternoonEnd,
            MinimumLunchBreak = entry.MinimumLunchBreak
        };
    }
}