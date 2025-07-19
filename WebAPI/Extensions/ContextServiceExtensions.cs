using Infrastructure.Data.Context;

namespace WebAPI.Extensions;

public static class ContextServiceExtensions
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, string csvPath)
        where TContext : class
    {
        var fullPath = Path.GetFullPath(csvPath);

        services.AddSingleton<IContext>(new AppDbContext(fullPath));

        return services;
    }
}

