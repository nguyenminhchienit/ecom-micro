using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories.Interfaces;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly StockGrpcService _stockGrpcService;


        public BasketsController(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint, 
            IMapper mapper, StockGrpcService stockGrpcService)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _stockGrpcService = stockGrpcService ?? throw new ArgumentNullException(nameof(stockGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> GetBasket([Required] string username)
        {
            var result = await _basketRepository.GetBasketByUserName(username);

            return Ok(result ?? new Cart(username));
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> UpdateBasket([FromBody] Cart basket)
        {
            // Comunicate grpc inventory to get stock
            foreach (var item in basket.Items) {
                var stock = await _stockGrpcService.GetStock(item.ProductNo);
                item.AvailableQuantity = stock.Quantity;
            }

            var options = new DistributedCacheEntryOptions()
             .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(10))
             .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            var result = await _basketRepository.UpdateBasket(basket, options);
            return Ok(result);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
        {
            var result = await _basketRepository.DeleteBasketFromUserName(username);
            return Ok(result);
        }

        [Route(template: "[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketBody)
        {
            var basket = await _basketRepository.GetBasketByUserName(basketBody.UserName);
            if (basket == null) return NotFound();

            //Publish checkout event to EventBus
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketBody);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);


            await _basketRepository.DeleteBasketFromUserName(basketBody.UserName);

            return Accepted();
        }
    }
}
