namespace TimeCalculator.Domain.Entities
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan MorningStart { get; set; }
        public TimeSpan MorningEnd { get; set; }
        public TimeSpan AfternoonStart { get; set; }
        public TimeSpan AfternoonEnd { get; set; }
        public TimeSpan MinimumLunchBreak { get; set; } = TimeSpan.FromMinutes(45); // Pause déjeuner minimum par défaut

        // Méthodes de calcul
        public TimeSpan CalculateMorningDuration()
            => MorningEnd - MorningStart;

        public TimeSpan CalculateAfternoonDuration()
            => AfternoonEnd - AfternoonStart;

        public TimeSpan CalculateLunchBreakDuration()
            => AfternoonStart - MorningEnd;

        public TimeSpan CalculateTotalWorkDuration()
            => CalculateMorningDuration() + CalculateAfternoonDuration();

        public bool IsLunchBreakValid()
            => CalculateLunchBreakDuration() >= MinimumLunchBreak;
    }
}
