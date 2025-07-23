namespace Application.DTOs.Camera;

public class CameraGroupedDTO
{
    public List<CameraDTO> DivisibleBy3 { get; set; } = [];
    public List<CameraDTO> DivisibleBy5 { get; set; } = [];
    public List<CameraDTO> DivisibleBy3And5 { get; set; } = [];
    public List<CameraDTO> NotDivisible { get; set; } = [];
}
