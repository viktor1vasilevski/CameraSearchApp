using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string csvPath)
    {
        // Register file-based context with path
        services.AddSingleton<IContext>(new AppDbContext(csvPath));

        // Application.Interfaces > Infrastructure.Data.Repositories
        services.AddScoped<ICameraRepository, CameraRepository>();

        // Application.Interfaces > Application.Services
        services.AddScoped<ICameraService, CameraService>();

        return services;
    }
}
