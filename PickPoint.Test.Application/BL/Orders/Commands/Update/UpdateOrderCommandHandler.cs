using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Orders.Commands.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<UpdateOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<UpdateOrderCommandHandler>();
    }

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id.Value, cancellationToken);

        if(order is null)
        {
            _logger.LogError("Order not found");
            return new Result(ErrorCode.NotFound, $"Order not found");
        }

        var postamat = await _unitOfWork.PostamatRepository.GetByIdAsync(request.PostamatNumber.Value, cancellationToken);

        if (postamat is null)
        {
            _logger.LogError("Postamat not found");
            return new Result(ErrorCode.NotFound, $"Postamat not found");
        }

        if (!postamat.IsActive)
        {
            _logger.LogError("Postamat not active");
            return new Result(ErrorCode.BadRequest, $"Postamat not active");
        }

        if (!request.Status.HasValue)
        {
            request.Status = DTOs.StatusDTO.Registered;
        }

        order.RecipientPhoneNumber = request.RecipientPhoneNumber;
        order.Recipient = request.Recipient;
        if(postamat.Id != order.PostamatId)
        {
            order.PostamatId = postamat.Id;
            order.Postamat = postamat;
        }
        order.Status = (Domain.Status)request.Status;
        order.Items = request.Items.ToList();
        order.Price = request.Price;

        await _unitOfWork.OrderRepository.UpdateAsync(order, cancellationToken);

        return Result.Empty;
    }
}
