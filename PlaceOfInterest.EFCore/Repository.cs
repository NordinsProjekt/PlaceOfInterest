using PlaceOfInterest.Application.Interfaces;

namespace PlaceOfInterest.EFCore;

public class Repository<T>(PlaceOfInterestContext context) : IRepository<T>
    where T : class
{
    public async Task<T> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id) ?? throw new NullReferenceException();
    }

    public List<T> GetAll<T1, TKey>(int skip, int take, Func<T, TKey> orderByKey)
    {
        return context.Set<T>().OrderBy(orderByKey).Skip(skip).Take(take).ToList();
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
