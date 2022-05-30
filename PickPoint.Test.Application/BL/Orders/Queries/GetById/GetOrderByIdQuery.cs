using FluentValidation;
using MediatR;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Orders.Queries.GetById;

public class GetOrderByIdQuery : IRequest<Result<OrderDTO>>
{
    public long? Id { get; set; }
}

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(i => i.Id).NotNull();
    }
}