using Microsoft.Extensions.DependencyInjection;
using TimeCalculator.Application;
using TimeCalculator.Infrastructure;
using TimeCalculator.Desktop.Views;
using System.Windows;
using TimeCalculator.Desktop.ViewModels;

namespace TimeCalculator.Desktop
{
    // Nous précisons System.Windows.Application pour éviter l'ambiguïté
    public partial class App : System.Windows.Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Configuration des services de l'application
            services.AddApplication();
            services.AddInfrastructure("Data Source=timeentries.db");

            // Enregistrement des ViewModels
            services.AddScoped<TimeEntryViewModel>();
            services.AddScoped<MainViewModel>();  // N'oubliez pas d'ajouter le MainViewModel

            // Enregistrement de la fenêtre principale
            services.AddScoped<MainWindow>();

            var serviceProvider = services.BuildServiceProvider();

            // Initialisation de la base de données
            await Infrastructure.DependencyInjection.InitializeDatabaseAsync(serviceProvider);

            // Démarrage de la fenêtre principale avec son ViewModel
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;  // Attribution du ViewModel à la fenêtre
            mainWindow.Show();
        }
    }
}