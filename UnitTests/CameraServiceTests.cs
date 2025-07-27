using Application.DTOs.Camera;
using Application.Enums;
using Application.Interfaces.Repositories;
using Application.Requests.Camera;
using Application.Services;
using Domain.Entities;
using Infrastructure.Exceptions.Csv;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public class CameraServiceTests
    {
        private readonly Mock<ICameraRepository> _cameraRepositoryMock;
        private readonly Mock<ILogger<CameraService>> _loggerMock;
        private readonly CameraService _service;

        public CameraServiceTests()
        {
            _cameraRepositoryMock = new Mock<ICameraRepository>();
            _loggerMock = new Mock<ILogger<CameraService>>();
            _service = new CameraService(_cameraRepositoryMock.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task GetGroupedCamerasAsync_ReturnsGroupedCameras_Success()
        {
            // Arrange
            var cameras = new List<Camera>
            {
                new Camera { Name = "UTR-CM-15", Latitude = 1, Longitude = 1 },  // Number = 15 divisible by 3 and 5
                new Camera { Name = "UTR-CM-9", Latitude = 2, Longitude = 2 },   // Number = 9 divisible by 3
                new Camera { Name = "UTR-CM-10", Latitude = 3, Longitude = 3 },  // Number = 10 divisible by 5
                new Camera { Name = "UTR-CM-7", Latitude = 4, Longitude = 4 }    // Number = 7 not divisible
            };

            _cameraRepositoryMock.Setup(r => r.LoadCsvAsync()).ReturnsAsync(cameras);

            // Act
            var result = await _service.GetGroupedCamerasAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data.DivisibleBy3And5, c => c.Number == 15);
            Assert.Contains(result.Data.DivisibleBy3, c => c.Number == 9);
            Assert.Contains(result.Data.DivisibleBy5, c => c.Number == 10);
            Assert.Contains(result.Data.NotDivisible, c => c.Number == 7);
        }

        [Fact]
        public async Task SearchCamerasByNameAsync_ReturnsFilteredCameras_Success()
        {
            // Arrange
            var cameras = new List<Camera>
            {
                new Camera { Name = "UTR-CM-15", Latitude = 1, Longitude = 1 },
                new Camera { Name = "UTR-CM-9", Latitude = 2, Longitude = 2 },
                new Camera { Name = "UTR-CM-10", Latitude = 3, Longitude = 3 },
                new Camera { Name = "UTR-CM-7", Latitude = 4, Longitude = 4 }
            };
        
            _cameraRepositoryMock.Setup(r => r.LoadCsvAsync()).ReturnsAsync(cameras);

            var request = new CameraRequest { Name = "9" }; // Should match "UTR-CM-9"

            // Act
            var result = await _service.SearchCamerasByNameAsync(request);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("UTR-CM-9", result.Data[0].Name);
        }

        [Fact]
        public async Task SearchCamerasByNameAsync_WithEmptyName_ReturnsAllCameras()
        {
            // Arrange
            var cameras = new List<Camera>
            {
                new Camera { Name = "UTR-CM-15", Latitude = 1, Longitude = 1 },
                new Camera { Name = "UTR-CM-9", Latitude = 2, Longitude = 2 },
                new Camera { Name = "UTR-CM-10", Latitude = 3, Longitude = 3 },
                new Camera { Name = "UTR-CM-7", Latitude = 4, Longitude = 4 }
            };
            _cameraRepositoryMock.Setup(r => r.LoadCsvAsync()).ReturnsAsync(cameras);

            var request = new CameraRequest { Name = "" }; // empty filter

            // Act
            var result = await _service.SearchCamerasByNameAsync(request);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(cameras.Count, result.Data.Count);
        }

        [Fact]
        public async Task GetGroupedCamerasAsync_WhenCsvParseException_ReturnsErrorResponse()
        {
            // Arrange
            _cameraRepositoryMock
                .Setup(r => r.LoadCsvAsync())
                .ThrowsAsync(new CsvParseException("CSV parse error"));

            // Act
            var result = await _service.GetGroupedCamerasAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("The camera data format is invalid. Please contact support.", result.Message);
            Assert.Equal(NotificationType.ServerError, result.NotificationType);
        }

        [Fact]
        public async Task SearchCamerasByNameAsync_WhenDataLoadException_ReturnsErrorResponse()
        {
            // Arrange
            _cameraRepositoryMock
                .Setup(r => r.LoadCsvAsync())
                .ThrowsAsync(new DataLoadException("Data load error"));

            var request = new CameraRequest { Name = "test" };

            // Act
            var result = await _service.SearchCamerasByNameAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Unable to load camera data. Try again later.", result.Message);
            Assert.Equal(NotificationType.ServerError, result.NotificationType);
        }
    }
}
