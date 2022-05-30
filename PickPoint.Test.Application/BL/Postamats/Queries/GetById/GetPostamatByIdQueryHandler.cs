using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Postamats.Queries.GetById;

public class GetPostamatByIdQueryHandler : IRequestHandler<GetPostamatByIdQuery, Result<PostamatDTO>>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<GetPostamatByIdQueryHandler> _logger;

    public GetPostamatByIdQueryHandler(IPickPointUnitOfWork unitOfWork, ILogger<GetPostamatByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<GetPostamatByIdQueryHandler>();
    }

    public async Task<Result<PostamatDTO>> Handle(GetPostamatByIdQuery request, CancellationToken cancellationToken)
    {
        var postamat = await _unitOfWork.PostamatRepository.GetByIdAsync(request.Id.Value, cancellationToken);

        if(postamat is null)
        {
            _logger.LogError("Postamat not found");
            return new Result<PostamatDTO>(null, ErrorCode.NotFound, $"Postamat not found");
        }

        return new Result<PostamatDTO>(new PostamatDTO()
        {
            Address = postamat.Address,
            IsActive = postamat.IsActive,
            Orders = postamat.Orders.Select(y => new OrderDTO()
            {
                Items = y.Items.ToList().AsReadOnly(),
                PostamatId = y.PostamatId,
                Price = y.Price,
                Recipient = y.Recipient,
                RecipientPhoneNumber = y.RecipientPhoneNumber,
                Status = (StatusDTO)y.Status
            }).ToList().AsReadOnly()
        });
    }
}
