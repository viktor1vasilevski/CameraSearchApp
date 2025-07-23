using Infrastructure.Exceptions.Configuration;
using Microsoft.Extensions.Configuration;
using Infrastructure.IoC;
using Microsoft.Extensions.Hosting;
using Search.Constants;

namespace SearchConsole.Helpers
{
    public static class HostBuilderHelper
    {
        public static IHost CreateHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var csvRelativePath = context.Configuration.GetValue<string>("CsvSettings:CsvPath");

                    if (string.IsNullOrWhiteSpace(csvRelativePath))
                        throw new CsvConfigurationMissingException(CsvConfigurationConstants.CsvPathMissingLog);

                    var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, csvRelativePath));

                    services.AddInfrastructureServices(fullPath);
                })
                .Build();
        }
    }
}
