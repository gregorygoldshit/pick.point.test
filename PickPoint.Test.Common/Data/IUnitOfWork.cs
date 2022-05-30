namespace PickPoint.Test.Common.Data;

public interface IUnitOfWork : IDisposable
{
    Task<IDisposable> BeginAsync(CancellationToken cancellationToken);

    Task CommitAsync(CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
