using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string csvPath)
    {
        var fullPath = Path.GetFullPath(csvPath);
        services.AddSingleton<ICameraRepository>(new CsvCameraRepository(fullPath));

        services.AddScoped<ICameraService, CameraService>();

        return services;
    }
}
