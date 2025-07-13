namespace PlaceOfInterest.Application.Interfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(Guid id);
    List<TEntity> GetAll<T, TKey>(int skip, int take, Func<TEntity, TKey> orderByKey);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();

    IQueryable<TEntity> GetQuery();
    List<TEntity> RunQuery(IQueryable<TEntity> query);
}
