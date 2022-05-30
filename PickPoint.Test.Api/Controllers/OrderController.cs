using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Test.Application.BL.Orders.Commands.Cancel;
using PickPoint.Test.Application.BL.Orders.Commands.Create;
using PickPoint.Test.Application.BL.Orders.Commands.Update;
using PickPoint.Test.Application.BL.Orders.Queries.GetById;
using PickPoint.Test.Application.DTOs;
using PickPoint.Test.Common.DTOs;
using PickPoint.Test.Common.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PickPoint.Test.Api.Controllers
{
    /// <summary>
    /// Управление заказами
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <param name="request">Запрос заказа</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Информация о заказе</returns>
        /// <response code="200">Информация о заказе</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="500">Необработанная ошибка</response>
        [ProducesResponseType(typeof(IReadOnlyCollection<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return result.ToHttpResponse();
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="request">Запрос на создание заказа</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="500">Необработанная ошибка</response>
        [ProducesResponseType(typeof(IReadOnlyCollection<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return result.ToHttpResponse();
        }

        /// <summary>
        /// Отмена заказа
        /// </summary>
        /// <param name="request">Запрос на отмену заказа</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="500">Необработанная ошибка</response>
        [ProducesResponseType(typeof(IReadOnlyCollection<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Cancel([FromQuery] CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return result.ToHttpResponse();
        }

        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <param name="request">Запрос на обновление заказа</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="500">Необработанная ошибка</response>
        [ProducesResponseType(typeof(IReadOnlyCollection<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [HttpPatch]
        public async Task<IActionResult> Update([FromQuery] UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return result.ToHttpResponse();
        }
    }
}
