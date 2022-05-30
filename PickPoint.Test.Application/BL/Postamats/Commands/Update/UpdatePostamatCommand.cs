using MediatR;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Postamats.Commands.Update;

public class UpdatePostamatCommand : IRequest<Result>
{
    public long? Id { get; set; }
    public string Address { get; set; }
}
