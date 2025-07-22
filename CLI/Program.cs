using Application.Requests.Camera;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Search.Extensions;
using Search.Helpers;

class Program
{
    static int Main(string[] args)
    {
        var host = HostBuilderExtensions.CreateHost(args);
        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        var nameFilter = args.GetNameArgument();
        if (string.IsNullOrWhiteSpace(nameFilter))
        {
            Console.WriteLine("You must enter a search term.");
            return 1;
        }

        try
        {
            host.Services.UseCameraService(service =>
            {
                var response = service.GetCameras(new CameraRequest { Name = nameFilter });

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
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred in Program.Main.");
            ErrorPrintHelper.PrintErrorMessage("A fatal error occurred. Please check the logs for details.");
            return 2;
        }
    }
}
