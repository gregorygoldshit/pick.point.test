using PickPoint.Test.Common.Data;
using PickPoint.Test.Infrastructure.Interfaces;

namespace PickPoint.Test.Infrastructure.UnitOfWork;

public interface IPickPointUnitOfWork : IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IPostamatRepository PostamatRepository { get; }
}
