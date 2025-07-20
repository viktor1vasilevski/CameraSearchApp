using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;


public class BaseRepository<TEntity>(IContext context) : IBaseRepository<TEntity> where TEntity : class
{
    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        return await context.LoadDataAsync<TEntity>();
    }
}
