using MediatR;
using Microsoft.Extensions.Logging;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Deactivate;

public class DeactivatePostamatCommandHandler : IRequestHandler<DeactivatePostamatCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<DeactivatePostamatCommandHandler> _logger;

    public DeactivatePostamatCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<DeactivatePostamatCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(DeactivatePostamatCommand request, CancellationToken cancellationToken)
    {
        var postamat = await _unitOfWork.PostamatRepository.GetByIdAsync(request.Id, cancellationToken);

        if(postamat is null)
        {
            _logger.LogError("Postamat not found");
            return new Result(ErrorCode.NotFound, $"Postamat not found");
        }

        postamat.IsActive = false;

        await _unitOfWork.PostamatRepository.UpdateAsync(postamat, cancellationToken);

        return Result.Empty;
    }
}
