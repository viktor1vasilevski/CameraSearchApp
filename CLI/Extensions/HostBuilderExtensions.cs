using Infrastructure.Exceptions.Configuration;
using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Search.Constants;

namespace Search.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHost CreateHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var csvRelativePath = context.Configuration.GetValue<string>("CsvSettings:CsvPath");

                    if (string.IsNullOrWhiteSpace(csvRelativePath))
                        throw new CsvConfigurationMissingException(CsvConfigurationConstants.CsvPathIsEmptyOrMissing);

                    var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, csvRelativePath));

                    services.AddInfrastructureServices(fullPath);
                })
                .Build();
        }
    }
}
