namespace Application.DTOs.Camera;

public class CameraDTO
{
    public int Number { get; set; }
    public string Name { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }


    public void Print()
    {
        Console.WriteLine($"{Number} | {Name} | {Latitude} | {Longitude}");
    }
}
