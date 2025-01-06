using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using TimeCalculator.Domain.Interfaces;
using TimeCalculator.Infrastructure.Data;
using TimeCalculator.Infrastructure.Repositories;

namespace TimeCalculator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        // Configuration de la base de données SQLite
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        // Enregistrement du repository
        services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();

        return services;
    }

    // Méthode pour assurer la création de la base de données
    public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite("Data Source=TimeCalculator.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}