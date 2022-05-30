using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Orders.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<CancelOrderCommandHandler> _logger;
    public CancelOrderCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<CancelOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<CancelOrderCommandHandler>();
    }

    public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id.Value, cancellationToken);
        if(order is null)
        {
            _logger.LogError("Order not found");
            return new Result(ErrorCode.NotFound, $"Order not found");
        }

        order.Status = Domain.Status.Canceled;

        await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

        return Result.Empty;
    }
}
