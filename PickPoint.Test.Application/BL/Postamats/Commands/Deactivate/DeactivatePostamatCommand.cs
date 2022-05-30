using MediatR;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Deactivate;

public class DeactivatePostamatCommand : IRequest<Result>
{
    public long Id { get; set; }
}
