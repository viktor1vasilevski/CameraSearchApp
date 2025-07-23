using Application.Requests.Camera;
using Microsoft.Extensions.Hosting;
using Search.Extensions;
using Search.Helpers;

namespace SearchConsole.Helpers;

public static class CameraSearchHelper
{
    public static async Task RunCameraSearchAsync(IHost host, string nameFilter)
    {
        await host.Services.UseCameraServiceAsync(async service =>
        {
            var response = await service.SearchCamerasByNameAsync(new CameraRequest { Name = nameFilter });

            if (response.Success && response.Data is { Count: > 0 })
            {
                CameraPrintHelper.PrintCameras(response.Data);
            }
            else
            {
                MessagePrintHelper.PrintMessage("No cameras found matching your search.", ConsoleColor.Blue);
            }
        });
    }
}
