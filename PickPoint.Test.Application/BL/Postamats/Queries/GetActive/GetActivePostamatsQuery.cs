using MediatR;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.Models;

namespace PickPoint.Test.Application.BL.Postamats.Queries.GetActive;

public class GetActivePostamatsQuery : IRequest<Result<IReadOnlyCollection<PostamatDTO>>>
{
}
