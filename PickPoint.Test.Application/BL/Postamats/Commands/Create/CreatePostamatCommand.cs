using MediatR;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Create;

public class CreatePostamatCommand : IRequest<Result>
{
    public string Address { get; set; }
    public bool IsActive { get; set; }
}
