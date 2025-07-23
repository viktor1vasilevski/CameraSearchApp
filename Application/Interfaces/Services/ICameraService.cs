using Application.DTOs.Camera;
using Application.Requests.Camera;
using Application.Responses;

namespace Application.Interfaces.Services;

public interface ICameraService
{
    ApiResponse<CameraGroupedDTO> GetCameras();
    ApiResponse<List<CameraDTO>> GetFilteredCameras(CameraRequest request);
}
