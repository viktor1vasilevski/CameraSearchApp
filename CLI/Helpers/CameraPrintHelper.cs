using Application.DTOs.Camera;

namespace Search.Helpers;

public static class CameraPrintHelper
{
    public static void PrintCameras(List<CameraDTO> cameras)
    {
        if (!cameras.Any())
        {
            Console.WriteLine("No cameras found matching your search.");
        }
        else
        {
            foreach (var cam in cameras.OrderBy(c => c.Number))
            {
                cam.Print();
            }
        }
    }
}
