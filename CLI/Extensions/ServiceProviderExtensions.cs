using Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Search.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task UseCameraServiceAsync(this IServiceProvider services, Func<ICameraService, Task> action)
    {
        using var scope = services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ICameraService>();
        await action(service);
    }
}
