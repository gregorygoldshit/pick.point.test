using PickPoint.Test.Common.Data;
using PickPoint.Test.Domain;

namespace PickPoint.Test.Infrastructure.Interfaces;

public interface IPostamatRepository : IRepository<Postamat>
{
    public Task<IReadOnlyCollection<Postamat>> GetActive(CancellationToken cancellationToken);
    public new Task<Postamat> GetByIdAsync(long id, CancellationToken cancellationToken);
}
