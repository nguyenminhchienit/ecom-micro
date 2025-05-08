using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Featutes.V1.Orders.Queries.GetOrders;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        private static class RouteNames
        {
            public const string GetOrder = nameof(GetOrder);
        }

        [HttpGet("{userName}", Name = RouteNames.GetOrder)]
        [ProducesResponseType(typeof(IEnumerable<OrderRequest>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderRequest>>> GetOrdersByUserName([Required] string userName)
        {
            var query = new GetOrdersQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

    }
}
