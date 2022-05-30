using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Test.Application.BL.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDTO>>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<GetOrderByIdQueryHandler> _logger;

    public GetOrderByIdQueryHandler(IPickPointUnitOfWork unitOfWork, ILogger<GetOrderByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<GetOrderByIdQueryHandler>();
    }

    public async Task<Result<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id.Value, cancellationToken);

        if(order is null)
        {
            _logger.LogError("Order not found");
            return new Result<OrderDTO>(null, ErrorCode.NotFound, $"Order not found");
        }

        return new Result<OrderDTO>(new OrderDTO()
        {
            Items = order.Items.ToList().AsReadOnly(),
            PostamatId = order.PostamatId,
            Price = order.Price,
            Recipient = order.Recipient,
            RecipientPhoneNumber = order.RecipientPhoneNumber,
            Status = (StatusDTO)order.Status
        });
    }
}