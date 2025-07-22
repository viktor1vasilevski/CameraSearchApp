using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Search.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHost CreateHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddJsonFile("appsettings.Development.json", optional: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var csvRelativePath = context.Configuration.GetValue<string>("CsvSettings:CsvPath");
                    var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, csvRelativePath));

                    services.AddInfrastructureServices(fullPath);
                })
                .Build();
        }
    }
}
