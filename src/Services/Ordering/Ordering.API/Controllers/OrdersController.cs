using AutoMapper;
using Contract.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Featutes.V1.Orders.Commands.CreateOrder;
using Ordering.Application.Featutes.V1.Orders.Commands.DeleteOrder;
using Ordering.Application.Featutes.V1.Orders.Commands.UpdateOrder;
using Ordering.Application.Featutes.V1.Orders.Queries.GetOrders;
using Shared.SeedWork;
using Shared.Services.Email;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISmtpEmailService smtpEmailService;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, ISmtpEmailService smtpEmailService, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.smtpEmailService = smtpEmailService;
            _mapper = mapper;
        }

        private static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
            public const string GetOrder = nameof(GetOrder);
            public const string CreateOrder = nameof(CreateOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string DeleteOrder = nameof(DeleteOrder);
            public const string DeleteOrderByDocumentNo = nameof(DeleteOrderByDocumentNo);
        }


        [HttpGet("username/{userName}", Name = RouteNames.GetOrder)]
        [ProducesResponseType(typeof(IEnumerable<OrderRequest>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderRequest>>> GetOrdersByUserName([Required] string userName)
        {
            var query = new GetOrdersQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost(Name = RouteNames.CreateOrder)]
        [ProducesResponseType(typeof(ApiResult<long>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApiResult<long>>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:long}", Name = RouteNames.UpdateOrder)]
        [ProducesResponseType(typeof(ApiResult<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderDto>> UpdateOrder([Required] long id, [FromBody] UpdateOrderCommand command)
        {
            command.SetId(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:long}", Name = RouteNames.DeleteOrder)]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteOrder([Required] long id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet(template: "test-email")]
        public async Task<IActionResult> TestEmail()
        {
            var message = new MailRequest
            {
                Body = "<h1>hello</h1>",
                Subject = "Test",
                ToAddress = "takistechcommunity@gmail.com"
            };

            await smtpEmailService.SendEmailAsync(message);

            return Ok();
        }


    }
}
