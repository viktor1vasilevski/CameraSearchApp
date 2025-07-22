using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Search.Extensions;

public static class ServiceProviderExtensions
{
    public static void UseCameraService(this IServiceProvider services, Action<ICameraService> action)
    {
        var service = services.GetRequiredService<ICameraService>();
        action(service);
    }
}
