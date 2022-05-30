using FluentValidation;
using MediatR;
using PickPoint.Test.Common.Models;
using ValidationException = FluentValidation.ValidationException;

namespace PickPoint.Test.Common.Mediatr.Pipelines;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : Result
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
            .Select(i => i.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(i => i != null)
            .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return next();
    }
}
