using PickPoint.Test.Common.Data;
using PickPoint.Test.Infrastructure.Interfaces;
using PickPoint.Test.Infrastructure.Repositories;

namespace PickPoint.Test.Infrastructure.UnitOfWork;

public class PickPointUnitOfWork : UnitOfWork<PickPointDbContext> , IPickPointUnitOfWork
{
    private IOrderRepository _OrderRepository;
    private IPostamatRepository _PostamatRepository;
    public IOrderRepository OrderRepository => _OrderRepository ??= new OrderRepository(DbContext);
    public IPostamatRepository PostamatRepository => _PostamatRepository ??= new PostamatRepository(DbContext);
    public PickPointUnitOfWork(PickPointDbContext dbContext) : base(dbContext)
    {
    }
}
