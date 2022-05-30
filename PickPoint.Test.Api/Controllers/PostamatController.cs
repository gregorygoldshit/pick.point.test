using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Test.Application.BL.Postamats.Commands.Create;
using PickPoint.Test.Application.BL.Postamats.Commands.Deactivate;
using PickPoint.Test.Application.BL.Postamats.Commands.Update;
using PickPoint.Test.Application.BL.Postamats.Queries.GetActive;
using PickPoint.Test.Application.BL.Postamats.Queries.GetById;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.DTOs;
using PickPoint.Test.Common.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PickPoint.Test.Api.Controllers;

/// <summary>
/// Управление постаматами
/// </summary>
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]/[action]")]
public class PostamatController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostamatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение информации о постамате
    /// </summary>
    /// <param name="request">Запрос на получение информации о постамате</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о постамате</returns>
    /// <response code="200">Информация о постамате</response>
    /// <response code="400">Некорректный запрос</response>
    /// <response code="500">Необработанная ошибка</response>
    [ProducesResponseType(typeof(IReadOnlyCollection<PostamatDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] GetPostamatByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.ToHttpResponse();
    }

    /// <summary>
    /// Получение списка активных постаматов
    /// </summary>
    /// <param name="request">Запрос списка активных постаматов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список активных постаматов</returns>
    /// <response code="200">Список активных постаматов</response>
    /// <response code="400">Некорректный запрос</response>
    /// <response code="500">Необработанная ошибка</response>
    [ProducesResponseType(typeof(IReadOnlyCollection<PostamatDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetActive([FromQuery] GetActivePostamatsQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.ToHttpResponse();
    }

    /// <summary>
    /// Создание постамата
    /// </summary>
    /// <param name="request">Запрос на создание постамата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="400">Некорректный запрос</response>
    /// <response code="500">Необработанная ошибка</response>
    [ProducesResponseType(typeof(IReadOnlyCollection<PostamatDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] CreatePostamatCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.ToHttpResponse();
    }

    /// <summary>
    /// Отключение постамата
    /// </summary>
    /// <param name="request">Запрос на отключение постамата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="400">Некорректный запрос</response>
    /// <response code="500">Необработанная ошибка</response>
    [ProducesResponseType(typeof(IReadOnlyCollection<PostamatDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Deactivate([FromQuery] DeactivatePostamatCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.ToHttpResponse();
    }

    /// <summary>
    /// Обновление данных постамата
    /// </summary>
    /// <param name="request">Запрос на обновление данных постамата</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="400">Некорректный запрос</response>
    /// <response code="500">Необработанная ошибка</response>
    [ProducesResponseType(typeof(IReadOnlyCollection<PostamatDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
    [HttpPatch]
    public async Task<IActionResult> Update([FromQuery] UpdatePostamatCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result.ToHttpResponse();
    }

}
