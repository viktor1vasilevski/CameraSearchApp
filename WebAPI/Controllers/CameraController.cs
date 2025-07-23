using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{


    [HttpGet("grouped")]
    public async Task<IActionResult> GetGroupedAsync()
    {
        var response = await cameraService.GetGroupedCamerasAsync();
        return HandleResponse(response);
    }
}
