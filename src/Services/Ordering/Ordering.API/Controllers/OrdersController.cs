using Contract.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Featutes.V1.Orders.Queries.GetOrders;
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

        public OrdersController(IMediator mediator, ISmtpEmailService smtpEmailService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.smtpEmailService = smtpEmailService;
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
