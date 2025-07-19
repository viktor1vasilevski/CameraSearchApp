using Application.DTOs.Camera;

namespace Application.Interfaces;

public interface ICameraService
{
    Task<List<CameraDTO>> GetCamerasAsync(string? name = null);
}
