using Application.Interfaces.Services;
using Application.Requests.Camera;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{


    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var response = await cameraService.GetCamerasAsync();
        return HandleResponse(response);
    }

    [HttpGet("Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] CameraRequest request)
    {
        var response = await cameraService.GetFilteredCamerasAsync(request);
        return HandleResponse(response);
    }
}
