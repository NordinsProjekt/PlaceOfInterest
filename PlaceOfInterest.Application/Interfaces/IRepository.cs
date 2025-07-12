namespace PlaceOfInterest.Application.Interfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}
