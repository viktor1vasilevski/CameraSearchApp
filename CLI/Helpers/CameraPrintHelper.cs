using Application.DTOs.Camera;

namespace Search.Helpers;

public static class CameraPrintHelper
{
    public static void PrintCameras(List<CameraDTO> cameras)
    {
        foreach (var cam in cameras.OrderBy(c => c.Number))
        {
            cam.Print();
        }
    }
}
