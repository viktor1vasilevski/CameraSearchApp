using Application.DTOs.Camera;
using Application.Responses;

namespace Application.Interfaces;

public interface ICameraService
{
    Task<ApiResponse<List<CameraDTO>>> GetCamerasAsync(string? name = null);
}
