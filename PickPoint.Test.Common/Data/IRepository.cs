using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Test.Common.Data;

public interface IRepository<T> where T : Entity
{
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<T>> GetByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken);

    Task InsertAsync(T entity, CancellationToken cancellationToken);

    Task InsertAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken);

    Task<bool> InsertIfNotExistsAsync(T entity, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken);

    Task RemoveAsync(T entity, CancellationToken cancellationToken);

    Task RemoveRangeAsync(IReadOnlyCollection<T> entities, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}
