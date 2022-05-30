using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PickPoint.Test.Common.Enums;
using PickPoint.Test.Common.Models;
using PickPoint.Test.Infrastructure.UnitOfWork;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Update;

public class UpdatePostamatCommandHandler : IRequestHandler<UpdatePostamatCommand, Result>
{
    private readonly IPickPointUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePostamatCommandHandler> _logger;

    public UpdatePostamatCommandHandler(IPickPointUnitOfWork unitOfWork, ILogger<UpdatePostamatCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger ?? new NullLogger<UpdatePostamatCommandHandler>();
    }

    public async Task<Result> Handle(UpdatePostamatCommand request, CancellationToken cancellationToken)
    {
        var postamat = await _unitOfWork.PostamatRepository.GetByIdAsync(request.Id.Value, cancellationToken);

        if (postamat is null)
        {
            _logger.LogError("Postamat not found");
            return new Result(ErrorCode.NotFound, $"Postamat not found");
        }

        postamat.Address = request.Address;

        await _unitOfWork.PostamatRepository.UpdateAsync(postamat, cancellationToken);
        return Result.Empty;
    }
}
