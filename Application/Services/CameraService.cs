using Application.Constants;
using Application.DTOs.Camera;
using Application.Enums;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests.Camera;
using Application.Responses;
using Infrastructure.Exceptions.Csv;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CameraService(ICameraRepository cameraRepository, ILogger<CameraService> logger) : ICameraService
{
    public ApiResponse<CameraGroupedDTO> GetCameras()
    {
        try
        {
            var cameras = cameraRepository.LoadCsv();

            var grouped = new CameraGroupedDTO();

            foreach (var cam in cameras)
            {
                var dto = new CameraDTO
                {
                    Number = cam.Number,
                    Name = cam.Name,
                    Latitude = cam.Latitude,
                    Longitude = cam.Longitude
                };

                if (cam.Number % 3 == 0 && cam.Number % 5 == 0)
                    grouped.DivisibleBy3And5.Add(dto);
                else if (cam.Number % 3 == 0)
                    grouped.DivisibleBy3.Add(dto);
                else if (cam.Number % 5 == 0)
                    grouped.DivisibleBy5.Add(dto);
                else
                    grouped.NotDivisible.Add(dto);
            }

            return new ApiResponse<CameraGroupedDTO>
            {
                Success = true,
                NotificationType = NotificationType.Success,
                Data = grouped,
            };
        }
        catch (CsvParseException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetCameras), DateTime.Now);

            return new ApiResponse<CameraGroupedDTO>
            {
                Success = false,
                Message = CameraConstants.DataFormatInvalid,
                NotificationType = NotificationType.ServerError
            };
        }
        catch (DataLoadException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetCameras), DateTime.Now);

            return new ApiResponse<CameraGroupedDTO>
            {
                Success = false,
                Message = CameraConstants.FailedLoadingCameraData,
                NotificationType = NotificationType.ServerError
            };
        }
    }

    public ApiResponse<List<CameraDTO>> GetFilteredCameras(CameraRequest request)
    {
        try
        {
            var cameras = cameraRepository.LoadCsv();

            cameras = cameras.WhereIf(!string.IsNullOrEmpty(request.Name),
                x => x.Name.Contains(request.Name!, StringComparison.OrdinalIgnoreCase));

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
                Data = camerasDTOs,
            };
        }
        catch (CsvParseException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetCameras), DateTime.Now);

            return new ApiResponse<List<CameraDTO>>
            {
                Success = false,
                Message = CameraConstants.DataFormatInvalid,
                NotificationType = NotificationType.ServerError
            };
        }
        catch (DataLoadException ex)
        {
            logger.LogError(ex, "Exception ocured in [{Function}] at [{Timestamp}]",
                nameof(GetCameras), DateTime.Now);

            return new ApiResponse<List<CameraDTO>>
            {
                Success = false,
                Message = CameraConstants.FailedLoadingCameraData,
                NotificationType = NotificationType.ServerError
            };
        }
    }
}
