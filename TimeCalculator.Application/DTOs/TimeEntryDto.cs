namespace TimeCalculator.Application.DTOs;

public class TimeEntryDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan MorningStart { get; set; }
    public TimeSpan MorningEnd { get; set; }
    public TimeSpan AfternoonStart { get; set; }
    public TimeSpan AfternoonEnd { get; set; }
    public TimeSpan MinimumLunchBreak { get; set; }

    // Propriétés calculées
    public TimeSpan MorningDuration => MorningEnd - MorningStart;
    public TimeSpan AfternoonDuration => AfternoonEnd - AfternoonStart;
    public TimeSpan LunchBreakDuration => AfternoonStart - MorningEnd;
    public TimeSpan TotalWorkDuration => MorningDuration + AfternoonDuration;
    public bool IsLunchBreakValid => LunchBreakDuration >= MinimumLunchBreak;
}