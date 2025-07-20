using Application.Enums;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult HandleResponse<TEntity>(ApiResponse<TEntity> response) where TEntity : class
    {
        return response.NotificationType switch
        {
            NotificationType.Success => Ok(response),
            NotificationType.ServerError => StatusCode(500, response),
            _ => Ok(response),
        };
    }
}
