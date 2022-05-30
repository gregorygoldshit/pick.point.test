using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Postamats.Queries.GetActive;

public class GetActivePostamatsQueryHandler : IRequestHandler<GetActivePostamatsQuery, Result<IReadOnlyCollection<PostamatDTO>>>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<GetActivePostamatsQueryHandler> _logger;

    public GetActivePostamatsQueryHandler(IPickPointUnitOfWork unitOfWork, ILogger<GetActivePostamatsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<GetActivePostamatsQueryHandler>();
    }

    public async Task<Result<IReadOnlyCollection<PostamatDTO>>> Handle(GetActivePostamatsQuery request, CancellationToken cancellationToken)
    {
        var activePostamats = await _unitOfWork.PostamatRepository.GetActive(cancellationToken);

        if (activePostamats is null)
        {
            _logger.LogError("Postamat not found");
            return new Result<IReadOnlyCollection<PostamatDTO>>(null, ErrorCode.NotFound, $"Postamat not found");
        }

        return new Result<IReadOnlyCollection<PostamatDTO>>(activePostamats.Select(x => new PostamatDTO()
        {
            Address = x.Address,
            IsActive = x.IsActive,
            Orders = x.Orders.Select(y => new OrderDTO()
            {
                Items = y.Items.ToList().AsReadOnly(),
                PostamatId = y.PostamatId,
                Price = y.Price,
                Recipient = y.Recipient,
                RecipientPhoneNumber = y.RecipientPhoneNumber,
                Status = (StatusDTO)y.Status
            }).ToList().AsReadOnly()
        }).ToList().AsReadOnly());
    }
}