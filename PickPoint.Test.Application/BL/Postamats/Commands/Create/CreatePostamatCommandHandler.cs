using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Create;

public class CreatePostamatCommandHandler : IRequestHandler<CreatePostamatCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePostamatCommandHandler> _logger;

    public CreatePostamatCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<CreatePostamatCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<CreatePostamatCommandHandler>();
    }

    public async Task<Result> Handle(CreatePostamatCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.PostamatRepository.InsertAsync(new Domain.Postamat()
        {
            Address = request.Address,
            IsActive = request.IsActive
        }, cancellationToken);

        return Result.Empty;
    }
}
