using TimeCalculator.Domain.Entities;

namespace TimeCalculator.Domain.Services
{
    public interface ITimeCalculatorService
    {
        // Méthode pour ajouter une entrée avec 4 horaires
        Task<TimeEntry> AddFullTimeEntryAsync(
            DateTime date,
            TimeSpan morningStart,
            TimeSpan morningEnd,
            TimeSpan afternoonStart,
            TimeSpan afternoonEnd);

        // Méthode pour ajouter une entrée avec pause déjeuner minimum
        Task<TimeEntry> AddTimeEntryWithMinLunchAsync(
            DateTime date,
            TimeSpan morningStart,
            TimeSpan eveningEnd,
            TimeSpan minimumLunchBreak);

        // Obtenir le total des heures travaillées sur une période
        Task<TimeSpan> GetTotalWorkTimeAsync(DateTime startDate, DateTime endDate);

        // Vérifier si une journée est valide (pause déjeuner respectée)
        Task<bool> ValidateDayScheduleAsync(DateTime date);

        // Obtenir une entrée par date
        Task<TimeEntry> GetTimeEntryAsync(DateTime date);

        // Obtenir toutes les entrées d'une période
        Task<IEnumerable<TimeEntry>> GetTimeEntriesAsync(DateTime startDate, DateTime endDate);
    }
}
