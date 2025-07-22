using Application.Interfaces.Services;
using Application.Requests.Camera;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CameraController(ICameraService cameraService) : BaseController
{


    [HttpGet]
    public IActionResult Get([FromQuery] CameraRequest request)
    {
        var response = cameraService.GetCameras(request);
        return HandleResponse(response);
    }
}
