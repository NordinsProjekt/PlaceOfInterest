using System.Linq.Expressions;

namespace PlaceOfInterest.Application.Interfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);

    List<TEntity> GetAll<T, TKey>(int skip, int take, Func<TEntity, TKey> orderByKey,
        params Expression<Func<TEntity, object>>[] includes);

    int CountMatches(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();

    IQueryable<TEntity> GetQuery();
    List<TEntity> RunQuery(IQueryable<TEntity> query);
}
