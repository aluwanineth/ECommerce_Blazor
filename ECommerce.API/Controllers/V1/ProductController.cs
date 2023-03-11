using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductController : BaseApiController
    {
 
        /// <summary>
        /// Retrieve all Products by their filted by page number and page size.
        /// </summary>
        /// <param name="filter">The filter of the desired Products</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/Product?PageNumber=1&PageSize=1
        ///
        /// </remarks>
        /// <response code="200">Returns all Products</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [ResponseCache(CacheProfileName = "ProductCache")]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllProductsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        /// <summary>
        /// Retrieve the product by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Product</param>
        /// <returns>A string status</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/Product/1
        ///
        /// </remarks>
        /// <response code="200">Returns all Products</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }
    }
}