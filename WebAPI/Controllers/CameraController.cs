using Application.Interfaces.Services;
using Application.Requests.Camera;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CameraRequest request)
    {
        var response = await cameraService.GetCamerasAsync(request);
        return HandleResponse(response);
    }
}
