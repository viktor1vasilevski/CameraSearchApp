using Application.Requests.Camera;
using Infrastructure.Exceptions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Search.Constants;
using Search.Extensions;
using Search.Helpers;

class Program
{
    static async Task<int> Main(string[] args)
    {
        ILogger<Program>? logger = null;
        try
        {
            var host = HostBuilderExtensions.CreateHost(args);
            logger = host.Services.GetRequiredService<ILogger<Program>>();

            var nameFilter = args.GetNameArgument();
            if (string.IsNullOrWhiteSpace(nameFilter))
            {
                Console.WriteLine("You must enter a search term.");
                return 1;
            }

            await host.Services.UseCameraServiceAsync(async service =>
            {
                var response = await service.GetFilteredCamerasAsync(new CameraRequest { Name = nameFilter });

                if (response.Success && response.Data is { Count: > 0 })
                {
                    CameraPrintHelper.PrintCameras(response.Data);
                }
                else
                {
                    ErrorPrintHelper.PrintErrorMessage(response.Message ?? "An unknown error occurred.");
                }
            });

            return 0;

        }
        catch (CsvConfigurationMissingException ex)
        {
            logger?.LogCritical(ex, CsvConfigurationConstants.CsvPathMissingLog);
            ErrorPrintHelper.PrintErrorMessage(CsvConfigurationConstants.CsvPathMissingUser);
            return 1;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, CsvConfigurationConstants.UnhandledExceptionLog);
            ErrorPrintHelper.PrintErrorMessage(CsvConfigurationConstants.UnhandledExceptionUser);
            return 2;
        }


    }
}
