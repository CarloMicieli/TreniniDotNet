using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSeeding.DataLoader.Records.Catalog.CatalogItems;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog.Core;
using TreniniDotNet.Catalog;

namespace DataSeeding.Clients.Catalog.CatalogItems
{
    public sealed class CatalogItemsClient
    {
        private CatalogItemsService.CatalogItemsServiceClient Client { get; }
        private Logger Log { get; }

        public CatalogItemsClient(GrpcChannel channel, Logger log)
        {
            Log = log;
            Client = new CatalogItemsService.CatalogItemsServiceClient(channel);
        }

        public async Task<int> SendCatalogItemsAsync(IEnumerable<CatalogItem> catalogItems)
        {
            var requests = catalogItems.Select(CatalogItemRequests.RequestFromRecord);

            Log.Debug("Sending catalog items...");

            try
            {
                var streamingClient = Client.CreateCatalogItems();

                foreach (var request in requests)
                {
                    Log.Debug("Sending catalog item {0} {1}", request.Brand, request.ItemNumber);
                    await streamingClient.RequestStream.WriteAsync(request);
                }

                await streamingClient.RequestStream.CompleteAsync();
                var response = await streamingClient.ResponseAsync;
                Log.Debug("{0} catalog item(s) has been created", response.Created);
                return response.Created;
            }
            catch (RpcException e)
            {
                Log.Error(e.Message);
                return 0;
            }
        }
    }
}
