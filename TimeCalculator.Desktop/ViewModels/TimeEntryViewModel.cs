// ViewModels/TimeEntryViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TimeCalculator.Application.DTOs;
using TimeCalculator.Application.Services.Interfaces;
using TimeCalculator.Desktop.Commands;
using TimeCalculator.Desktop.ViewModels.Base;


namespace TimeCalculator.Desktop.ViewModels
{
    public class TimeEntryViewModel : ViewModelBase
    {
        private readonly ITimeCalculatorService _timeCalculatorService;
        private TimeEntryDto _currentEntry;



    

        private DateTime _selectedDate2;
        private ObservableCollection<TimeEntryDto> _entries;

        public TimeEntryViewModel(ITimeCalculatorService timeCalculatorService)
        {
            _timeCalculatorService = timeCalculatorService;
            _selectedDate2 = DateTime.Today;
            _currentEntry = new TimeEntryDto
            {
                Date = _selectedDate2,
                MinimumLunchBreak = TimeSpan.FromMinutes(45)
            };
            _entries = new ObservableCollection<TimeEntryDto>();

            SaveCommand = new RelayCommand(async _ =>
            {
                System.Diagnostics.Debug.WriteLine("SaveCommand exécutée");  // Log
                try
                {
                    await _timeCalculatorService.AddTimeEntryAsync(_currentEntry);
                    System.Diagnostics.Debug.WriteLine("AddTimeEntryAsync réussie");  // Log
                    await LoadEntries();
                    System.Diagnostics.Debug.WriteLine("LoadEntries réussie");  // Log
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur: {ex.Message}");  // Log
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }, _ =>
            {
                var isValid = IsValid();
                System.Diagnostics.Debug.WriteLine($"IsValid retourne: {isValid}");  // Log
                return isValid;
            });

            LoadCommand = new RelayCommand(async _ => await LoadEntries());
        }

        private bool IsValid()
        {
            System.Diagnostics.Debug.WriteLine($"MorningStart: {CurrentEntry.MorningStart}");  // Log
            System.Diagnostics.Debug.WriteLine($"MorningEnd: {CurrentEntry.MorningEnd}");  // Log
            System.Diagnostics.Debug.WriteLine($"AfternoonStart: {CurrentEntry.AfternoonStart}");  // Log
            System.Diagnostics.Debug.WriteLine($"AfternoonEnd: {CurrentEntry.AfternoonEnd}");  // Log
            System.Diagnostics.Debug.WriteLine($"IsLunchBreakValid: {CurrentEntry.IsLunchBreakValid}");  // Log

            return CurrentEntry.IsLunchBreakValid &&
                   CurrentEntry.MorningStart < CurrentEntry.MorningEnd &&
                   CurrentEntry.AfternoonStart < CurrentEntry.AfternoonEnd;
        }

        public TimeEntryDto CurrentEntry
        {
            get => _currentEntry;
            set => SetProperty(ref _currentEntry, value);
        }

        public DateTime SelectedDate2
        {
            get => _selectedDate2;
            set
            {
                if (SetProperty(ref _selectedDate2, value))
                {
                    CurrentEntry.Date = value;
                }
            }
        }

        public ObservableCollection<TimeEntryDto> Entries
        {
            get => _entries;
            set => SetProperty(ref _entries, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        private async Task LoadEntries()
        {
            var startDate = DateTime.Today.AddDays(-7);
            var endDate = DateTime.Today;
            var entries = await _timeCalculatorService.GetTimeEntriesForPeriodAsync(startDate, endDate);
            Entries.Clear();
            foreach (var entry in entries)
            {
                Entries.Add(entry);
            }
        }
    }
}