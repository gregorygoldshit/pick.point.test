using FluentValidation;
using MediatR;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Models;
using System.Text.RegularExpressions;

namespace PickPoint.Test.Application.BL.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<Result>
{
    public StatusDTO? Status { get; set; }
    public List<string> Items { get; set; }
    public decimal Price { get; set; }
    public long? PostamatNumber { get; set; }
    public string RecipientPhoneNumber { get; set; }
    public string Recipient { get; set; }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(i => i.Status).NotNull();
        RuleFor(i => i.Items).NotNull().Must(x => x.Count < 10);
        RuleFor(i => i.PostamatNumber).NotNull();
        RuleFor(i => i.RecipientPhoneNumber)
            .NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .Length(15).WithMessage("PhoneNumber must not be equal 15 characters.")
            .Matches(new Regex(@"[\+]7\d{3}-\d{3}-\d{2}-\d{2}")).WithMessage("PhoneNumber not valid");
    }
}
