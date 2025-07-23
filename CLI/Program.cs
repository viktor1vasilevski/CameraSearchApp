using Infrastructure.Exceptions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Search.Constants;
using Search.Helpers;
using SearchConsole.Helpers;

class Program
{
    static async Task<int> Main(string[] args)
    {
        ILogger<Program>? logger = null;
        try
        {
            var host = HostBuilderHelper.CreateHost(args);
            logger = host.Services.GetRequiredService<ILogger<Program>>();

            var nameFilter = InputHelper.GetNameOrPrompt(args);

            await CameraSearchHelper.RunCameraSearchAsync(host, nameFilter);

            return 0;

        }
        catch (CsvConfigurationMissingException ex)
        {
            logger?.LogCritical(ex, CsvConfigurationConstants.CsvPathMissingLog);
            MessagePrintHelper.PrintMessage(CsvConfigurationConstants.CsvPathMissingUser);
            return 1;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, CsvConfigurationConstants.UnhandledExceptionLog);
            MessagePrintHelper.PrintMessage(CsvConfigurationConstants.UnhandledExceptionUser);
            return 2;
        }


    }
}
