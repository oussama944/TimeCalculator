using Microsoft.Extensions.DependencyInjection;
using TimeCalculator.Application.Services;
using TimeCalculator.Application.Services.Interfaces;

namespace TimeCalculator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITimeCalculatorService, TimeCalculatorService>();
        return services;
    }
}