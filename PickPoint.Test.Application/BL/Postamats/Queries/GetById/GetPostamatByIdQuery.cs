using FluentValidation;
using MediatR;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Postamats.Queries.GetById;

public class GetPostamatByIdQuery : IRequest<Result<PostamatDTO>>
{
    public long? Id { get; set; }
}

public class GetPostamatByIdQueryValidator : AbstractValidator<GetPostamatByIdQuery>
{
    public GetPostamatByIdQueryValidator()
    {
        RuleFor(i => i.Id).NotNull();
    }
}