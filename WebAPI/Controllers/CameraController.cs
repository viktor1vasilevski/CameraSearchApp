using Application.Interfaces.Services;
using Application.Requests.Camera;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{


    [HttpGet]
    public IActionResult Get()
    {
        var response = cameraService.GetCameras();
        return HandleResponse(response);
    }

    [HttpGet]
    public IActionResult Search([FromQuery] CameraRequest request)
    {
        var response = cameraService.GetFilteredCameras(request);
        return HandleResponse(response);
    }
}
