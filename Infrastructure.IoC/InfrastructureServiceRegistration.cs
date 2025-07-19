using Application.Interfaces;
using Application.Services;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string csvPath)
    {
        // Register file-based context with path
        services.AddSingleton<IContext>(new AppDbContext(csvPath));

        // Infrastructure.Data.Interfaces > Infrastructure.Data.Repositories
        services.AddScoped<ICameraRepository, CameraRepository>();

        // Application.Interfaces > Application.Services
        services.AddScoped<ICameraService, CameraService>();

        return services;
    }
}
