using System.Linq.Expressions;

namespace PlaceOfInterest.Application.Interfaces;

public interface IRepository<TEntity>
{
    TEntity GetById(Guid id, params Expression<Func<TEntity, object>>[] includes);

    List<TEntity> GetAll<T, TKey>(int skip, int take, Func<TEntity, TKey> orderByKey,
        params Expression<Func<TEntity, object>>[] includes);

    List<TEntity> GetAll<T, TKey>(int skip, int take, Func<TEntity, TKey> orderByKey, bool verifiedOnly,
        params Expression<Func<TEntity, object>>[] includes);

    int CountMatches(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveChangesAsync();

    IQueryable<TEntity> GetQuery();
    List<TEntity> RunQuery(IQueryable<TEntity> query);
}
