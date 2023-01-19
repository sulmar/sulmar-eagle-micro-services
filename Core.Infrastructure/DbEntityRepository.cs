using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure;

public class DbEntityRepository<T, TContext> : IEntityRepository<T>
    where T : BaseEntity, new()
    where TContext: DbContext
{
    protected readonly TContext context;
    protected DbSet<T> entities => context.Set<T>();

    public DbEntityRepository(TContext context) => this.context = context;

    public virtual async Task AddAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        T entity = new T { Id = id };
        
        context.Remove(entity); // context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }

   

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await entities.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await entities.FindAsync(id);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        context.Update(entity);

        await context.SaveChangesAsync();
    }
}