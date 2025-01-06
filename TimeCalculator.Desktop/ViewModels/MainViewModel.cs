using System;
using System.Windows.Input;
using TimeCalculator.Desktop.ViewModels.Base;
using TimeCalculator.Desktop.ViewModels;
using TimeCalculator.Desktop.Commands;

namespace TimeCalculator.Desktop.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Nous gardons une référence au ViewModel des entrées de temps
        private readonly TimeEntryViewModel _timeEntryViewModel;

        // Une propriété pour stocker le ViewModel actuellement affiché
        private ViewModelBase _currentViewModel = null!;

        public MainViewModel(TimeEntryViewModel timeEntryViewModel)
        {
            // Initialisation via injection de dépendances
            _timeEntryViewModel = timeEntryViewModel;

            // Par défaut, nous affichons la vue des entrées de temps
            CurrentViewModel = _timeEntryViewModel;

            // Commande pour naviguer vers la vue des entrées de temps
            ShowTimeEntryViewCommand = new RelayCommand(_ => ShowTimeEntryView());
        }

        // Propriété pour le ViewModel actuellement affiché
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        // Commande pour afficher la vue des entrées de temps
        public ICommand ShowTimeEntryViewCommand { get; }

        // Méthode pour changer la vue actuelle
        private void ShowTimeEntryView()
        {
            CurrentViewModel = _timeEntryViewModel;
        }
    }
}