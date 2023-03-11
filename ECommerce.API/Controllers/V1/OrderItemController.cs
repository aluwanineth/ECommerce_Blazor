using ECommerce.Application.Features.OrderDetail.Commands.CreateOrderDetail;
using ECommerce.Application.Features.OrderDetail.Commands.DeleteOrderDetailByIdCommand;
using ECommerce.Application.Features.OrderItem.Queries.GetOrderItemById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class OrderItemController : BaseApiController
    {
        /// <summary>
        /// Create a new Order Item.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A newly created new Order Item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1.0/OrderItem
        ///     {
        ///         "orderNo": "string",
        ///         "userId": "string"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created Order Detail</response>
        /// <response code="400">If the user Order detail is null</response>
        /// <response code="500">If error occurs</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// delete order item by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired order item</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1.0/OrderItem/1
        ///
        /// </remarks>
        /// <response code="200">Returns deleted id</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteOrderItemByIdCommand { Id = id }));
        }

        /// <summary>
        /// Retrieve the order item by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired order item</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1.0/OrderItem/1
        ///
        /// </remarks>
        /// <response code="200">Returns order item per id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOrderItemByIdQuery { Id = id }));
        }
    }
}