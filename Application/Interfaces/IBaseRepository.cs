using Microsoft.EntityFrameworkCore.Query;

namespace Application.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync();
}

