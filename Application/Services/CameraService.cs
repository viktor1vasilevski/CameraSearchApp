using Application.Constants;
using Application.DTOs.Camera;
using Application.Enums;
using Application.Extensions;
using Application.Interfaces;
using Application.Responses;
using Infrastructure.Exceptions.Csv;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CameraService(ICameraRepository cameraRepository, ILogger<CameraService> logger) : ICameraService
{
    public async Task<ApiResponse<List<CameraDTO>>> GetCamerasAsync(string? name = null)
    {
        try
        {
            var cameras = await cameraRepository.GetAsync();

            cameras = cameras.WhereIf(!string.IsNullOrEmpty(name),
                x => x.Name.Contains(name!, StringComparison.OrdinalIgnoreCase));

            var camerasDTOs = cameras.Select(x => new CameraDTO
            {
                Number = x.Number,
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            }).ToList();

            return new ApiResponse<List<CameraDTO>>
            {
                Success = true,
                NotificationType = NotificationType.Success,
                Data = camerasDTOs
            };
        }
        catch (CsvParseException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}] while processing name='{Name}'",  
                nameof(GetCamerasAsync), DateTime.Now, name);

            return new ApiResponse<List<CameraDTO>>
            {
                Success = false,
                Message = CameraConstants.DataFormatInvalid,
                NotificationType = NotificationType.ServerError
            };
        }
        catch (DataLoadException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}] while processing name='{Name}'",
                nameof(GetCamerasAsync), DateTime.Now, name);

            return new ApiResponse<List<CameraDTO>>
            {
                Success = false,
                Message = CameraConstants.FailedLoadingCameraData,
                NotificationType = NotificationType.ServerError
            };
        }

    }
}
