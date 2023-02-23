namespace EgorEmployees.RestApi.Repositories.Interfaces;

public interface IRepository<TKey, TEntity>
    where TKey : struct
    where TEntity : class
{
    Task<TKey> CreateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> GetAsync(TKey id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(TKey id);
}
