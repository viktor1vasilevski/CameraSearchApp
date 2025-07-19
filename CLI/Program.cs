using Search.Extensions;
using Search.Helpers;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var host = HostBuilderExtensions.CreateHost(args);

        var nameFilter = args.GetNameArgument();
        if (string.IsNullOrWhiteSpace(nameFilter))
        {
            Console.WriteLine("You must enter a search term.");
            return 1;
        }

        await host.Services.UseCameraServiceAsync(async service =>
        {
            var cameras = await service.GetCamerasAsync(nameFilter);
            CameraPrintHelper.PrintCameras(cameras);
        });

        return 0;
    }
}
