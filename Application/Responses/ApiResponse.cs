using Application.Enums;

namespace Application.Responses;

public class ApiResponse<TEntity> where TEntity : class
{
    public TEntity? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public NotificationType NotificationType { get; set; }
}
