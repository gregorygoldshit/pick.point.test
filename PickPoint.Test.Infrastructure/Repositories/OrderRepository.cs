using PickPoint.Test.Common.Data;
using PickPoint.Test.Infrastructure.Interfaces;

namespace PickPoint.Test.Infrastructure.Repositories;

public class OrderRepository : Repository<PickPointDbContext, Domain.Order>, IOrderRepository
{
    public OrderRepository(PickPointDbContext context) : base(context)
    {
    }
}
