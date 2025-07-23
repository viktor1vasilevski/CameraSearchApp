using Application.DTOs.Camera;
using Application.Requests.Camera;
using Application.Responses;

namespace Application.Interfaces.Services;

public interface ICameraService
{
    Task<ApiResponse<CameraGroupedDTO>> GetCamerasAsync();
    Task<ApiResponse<List<CameraDTO>>> GetFilteredCamerasAsync(CameraRequest request);
}
