using Grpc.Core;
using Inventory.Grpc.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace Inventory.Grpc.Services
{
    public class InventoryServiceImple : InventoryService.InventoryServiceBase
    {
        private readonly ILogger _logger;
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryServiceImple(ILogger logger, IInventoryRepository inventoryRepository)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task<StockReponse> GetStock(StockRequest request, ServerCallContext context)
        {
            var quantity = await _inventoryRepository.GetStockQuantity(request.ItemNo);
            return new StockReponse { Quantity = quantity };
        }
    }
}
