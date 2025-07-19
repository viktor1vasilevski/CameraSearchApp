using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICameraRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<Camera>> GetAsync();
}
