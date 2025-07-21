using Infrastructure.IoC;
using Microsoft.Extensions.Hosting;

namespace Search.Extensions;

public static class HostBuilderExtensions
{
    public static IHost CreateHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                //var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../../cameras-defb.csv"); // local
                var csvPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../cameras-defb.csv"));
                services.AddInfrastructureServices(csvPath);
            }).Build();

    }
}
