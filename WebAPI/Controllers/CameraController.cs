using Application.DTOs.Camera;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController(ICameraService cameraService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<CameraDTO>> Get([FromQuery] string? name)
        {
            var response = await cameraService.GetCamerasAsync(name);
            return response;
        }
    }
}
