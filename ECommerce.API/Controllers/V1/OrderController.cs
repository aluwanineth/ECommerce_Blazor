using ECommerce.Application.Features.OrderDetail.Commands.Checkout;
using ECommerce.Application.Features.Orders.Commands;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        public OrderController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        /// <summary>
        /// Create a new Order.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created new Order</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1.0/order
        ///     {
        ///         "orderNo": "string",
        ///         "userId": "string"
        ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created Order</response>
    /// <response code="400">If the user Order detail is null</response>
    /// <response code="500">If error occurs</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            command.UserId = _authenticatedUser.UserId;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Retrieve the order by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired order</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1.0/order/1
        ///
        /// </remarks>
        /// <response code="200">Returns order for desire id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        /// <summary>
        /// Checkout order.
        /// </summary>
        /// <param name="command">The ID of the desired order</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1.0/order/checkout
        ///
        /// </remarks>
        /// <response code="200">Returns order for desire id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}