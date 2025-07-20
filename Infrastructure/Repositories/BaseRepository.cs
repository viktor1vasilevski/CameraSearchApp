using Application.Interfaces;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;


public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly IContext _context;

    public BaseRepository(IContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        return await _context.LoadDataAsync<TEntity>();
    }
}
