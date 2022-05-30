using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PickPoint.Test.Common.Data;

public abstract class UnitOfWork<T> : IUnitOfWork where T : DbContext
{
    protected readonly T DbContext;

    private IDbContextTransaction _dbContextTransaction;

    public UnitOfWork(T dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<IDisposable> BeginAsync(CancellationToken cancellationToken)
    {
        _dbContextTransaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
        return _dbContextTransaction;
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        if (_dbContextTransaction is null)
            throw new Exception("Transaction not started");

        return _dbContextTransaction.CommitAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in DbContext.ChangeTracker.Entries())
        {
            if (changedEntity.Entity is Entity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                        break;

                    case EntityState.Modified:
                        entity.UpdatedAt = now;
                        break;
                }
            }
        }

        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContextTransaction?.Dispose();
        DbContext.Dispose();
    }
}
