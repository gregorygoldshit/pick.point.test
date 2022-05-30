using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace PickPoint.Test.Common.Mediatr.Pipelines;

public class ExceptionBehavior<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
    where TResponse : Result
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse, TException>> _logger;

    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse, TException>> logger)
    {
        _logger = logger ?? new NullLogger<ExceptionBehavior<TRequest, TResponse, TException>>();
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var response = (Result)Activator.CreateInstance(typeof(TResponse));

        switch (exception)
        {
            case ValidationException:
                response?.SerError(ErrorCode.Validation, exception.Message);
                break;
            default:
                response?.SerError(ErrorCode.UnDefined, exception.Message);
                break;
        }

        _logger.LogInformation("Error request. Name = {RequestName}. Response = {Response}. Error message = {ErrorMessage}", typeof(TRequest).Name, response, exception.Message);

        state.SetHandled((TResponse)response);
        return Task.CompletedTask;
    }
}
