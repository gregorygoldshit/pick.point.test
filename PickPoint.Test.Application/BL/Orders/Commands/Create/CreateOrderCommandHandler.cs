using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;


namespace PickPoint.Test.Application.BL.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<CreateOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<CreateOrderCommandHandler>();
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

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

        await _unitOfWork.OrderRepository.InsertAsync(new Domain.Order()
        {
            Status = (Domain.Status)request.Status.Value,
            Items = request.Items.ToList(),
            Postamat = postamat,
            PostamatId = postamat.Id,
            Price = request.Price,
            Recipient = request.Recipient,
            RecipientPhoneNumber = request.RecipientPhoneNumber

        }, cancellationToken);

        return Result.Empty;
    }
}
