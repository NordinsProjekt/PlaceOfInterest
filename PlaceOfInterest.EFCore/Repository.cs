using PlaceOfInterest.Application.Interfaces;

namespace PlaceOfInterest.EFCore;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly PlaceOfInterestContext _context;

    public Repository(PlaceOfInterestContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id) ?? throw new NullReferenceException();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
