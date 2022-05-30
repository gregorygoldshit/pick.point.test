using Microsoft.EntityFrameworkCore;
using PickPoint.Test.Common.Data;
using PickPoint.Test.Domain;
using PickPoint.Test.Infrastructure.Interfaces;

namespace PickPoint.Test.Infrastructure.Repositories;

public class PostamatRepository : Repository<PickPointDbContext, Domain.Postamat>, IPostamatRepository
{
    public PostamatRepository(PickPointDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<Postamat>> GetActive(CancellationToken cancellationToken)
    {
        return await Entities
            .Include(x => x.Orders)
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Postamat> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await Entities.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
