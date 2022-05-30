using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Test.Common.Data;

public abstract class Repository<TContext, T> : IRepository<T>
   where TContext : DbContext
   where T : Entity
{
    protected readonly TContext Context;
    protected readonly DbSet<T> Entities;

    public Repository(TContext context)
    {
        Context = context;
        Entities = context.Set<T>();
    }

    public virtual async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyCollection<T>> GetByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken)
    {
        return await Entities.Where(i => ids.Contains(i.Id)).ToListAsync(cancellationToken);
    }

    public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public async Task InsertAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken)
    {
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<bool> InsertIfNotExistsAsync(T entity, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var existsTask = predicate != null ? Entities.AnyAsync(predicate, cancellationToken) : Entities.AnyAsync(cancellationToken);
        var exists = await existsTask;

        if (!exists)
            await Entities.AddAsync(entity, cancellationToken);

        return !exists;
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Entities.Attach(entity);
        var entry = Context.Entry(entity);
        entry.State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken)
    {
        Entities.AttachRange(entities);
        foreach (var entity in entities)
        {
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        return Task.CompletedTask;
    }

    public virtual Task RemoveAsync(T entity, CancellationToken cancellationToken)
    {
        Entities.Remove(entity);

        return Task.CompletedTask;
    }

    public virtual Task RemoveRangeAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken)
    {
        Entities.RemoveRange(entities);

        return Task.CompletedTask;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var existsTask = predicate != null ? Entities.AnyAsync(predicate, cancellationToken) : Entities.AnyAsync(cancellationToken);
        var exists = await existsTask;

        return exists;
    }

    public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var countTask = predicate != null ? Entities.CountAsync(predicate, cancellationToken) : Entities.CountAsync(cancellationToken);
        var count = await countTask;

        return count;
    }
}
