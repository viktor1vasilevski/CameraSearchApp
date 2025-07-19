using Application.DTOs.Camera;
using Application.Extensions;
using Application.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Services;

public class CameraService(ICameraRepository cameraRepository) : ICameraService
{
    public async Task<List<CameraDTO>> GetCamerasAsync(string? name = null)
    {
        var cameras = (await cameraRepository.GetAsync())
            .WhereIf(!string.IsNullOrEmpty(name), x => x.Name.Contains(name!, StringComparison.OrdinalIgnoreCase));

        var camerasDTOs = cameras.Select(x => new CameraDTO
        {
            Number = x.Number,
            Name = x.Name,
            Latitude = x.Latitude,
            Longitude = x.Longitude
        }).ToList();

        return camerasDTOs;
    }
}
