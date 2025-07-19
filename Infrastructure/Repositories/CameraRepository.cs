using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CameraRepository(IContext context) : ICameraRepository
{
    public async Task<IEnumerable<Camera>> GetAsync()
    {
        return await context.LoadDataAsync();
    }
}
