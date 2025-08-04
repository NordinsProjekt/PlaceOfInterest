using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Domain.Interface;

namespace PlaceOfInterest.EFCore;

public class Repository<T>(PlaceOfInterestContext context) : IRepository<T>
    where T : class, IEntity
{
    public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes) query = query.Include(include);

        return query.First(e => e.Id == id);
    }


    public List<T> GetAll<TEntity, TKey>(int skip, int take, Func<T, TKey> orderByKey,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes) query = query.Include(include);

        return query.AsEnumerable()
            .OrderBy(orderByKey)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    public List<T> GetAll<T1, TKey>(int skip, int take, Func<T, TKey> orderByKey, bool verifiedOnly,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes) query = query.Include(include);

        return verifiedOnly
            ? query.AsEnumerable().Where(x => x.Verified)
                .OrderBy(orderByKey)
                .Skip(skip)
                .Take(take)
                .ToList()
            : query.AsEnumerable()
                .OrderBy(orderByKey)
                .Skip(skip)
                .Take(take)
                .ToList();
    }

    public int CountMatches(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Count(predicate);
    }

    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public IQueryable<T> GetQuery()
    {
        return context.Set<T>();
    }

    public List<T> RunQuery(IQueryable<T> query)
    {
        return query.ToList();
    }
}
