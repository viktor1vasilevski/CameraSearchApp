using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? name)
    {
        var response = await cameraService.GetCamerasAsync(name);
        return HandleResponse(response);
    }
}
