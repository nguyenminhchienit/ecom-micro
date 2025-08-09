using Inventory.Grpc;

namespace Basket.API.GrpcServices
{
    public class StockGrpcService
    {
        private readonly InventoryService.InventoryServiceClient _client;

        public StockGrpcService(InventoryService.InventoryServiceClient serviceClient)
        {
            _client = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
        }

        public async Task<StockReponse> GetStock(string itemNo)
        {
            try
            {
                StockRequest request = new StockRequest { ItemNo = itemNo };
                var rs = await _client.GetStockAsync(request);

                return new StockReponse
                {
                    Quantity = rs.Quantity
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}
