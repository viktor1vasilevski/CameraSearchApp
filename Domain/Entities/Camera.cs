using System.Text.RegularExpressions;

namespace Domain.Entities;

public class Camera
{
    public string Name { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int Number
    {
        get
        {
            var match = Regex.Match(Name, @"UTR-CM-(\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }
    }
}
