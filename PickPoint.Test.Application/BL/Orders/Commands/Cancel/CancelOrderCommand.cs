using FluentValidation;
using MediatR;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Orders.Commands.Cancel;

/// <summary>
/// Запрос на отмену заказа
/// </summary>
public class CancelOrderCommand : IRequest<Result>
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public long? Id { get; set; }
}

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(i => i.Id).NotNull();
    }
}
