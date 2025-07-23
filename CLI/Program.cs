using Application.Requests.Camera;
using Infrastructure.Exceptions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Search.Extensions;
using Search.Helpers;

class Program
{
    static int Main(string[] args)
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

            host.Services.UseCameraService(service =>
            {
                var response = service.GetFilteredCameras(new CameraRequest { Name = nameFilter });

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
            if (logger != null)
                logger.LogCritical(ex, "CSV path configuration is missing. Application cannot continue.");

            ErrorPrintHelper.PrintErrorMessage("Configuration error: CSV path is missing or invalid. Please check your appsettings or environment variables.");
            return 1;
        }
        catch (Exception ex)
        {
            if (logger != null)
                logger.LogError(ex, "Unhandled exception occurred in Program.Main.");

            ErrorPrintHelper.PrintErrorMessage("A fatal error occurred. Please check the logs for details.");
            return 2;
        }


    }
}
