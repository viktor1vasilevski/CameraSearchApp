using Application.DTOs.Camera;
using Application.Requests.Camera;
using Application.Responses;

namespace Application.Interfaces.Services;

public interface ICameraService
{
    ApiResponse<List<CameraDTO>> GetCameras(CameraRequest request);
}
