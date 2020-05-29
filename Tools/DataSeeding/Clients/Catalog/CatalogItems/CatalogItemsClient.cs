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

        public async Task NewCatalogItemAsync(CatalogItem catalogItem)
        {
            var request = CatalogItemRequests.From(catalogItem);

            Log.Debug("Sending catalog item : {0}", request);

            try
            {
                var response = await Client.CreateCatalogItemAsync(request);
                Log.Information("Catalog item {0} {1} created", catalogItem.Brand, catalogItem.ItemNumber);
                Log.Debug("Response was {0}", response);
            }
            catch (RpcException e)
            {
                Log.Error("Error {0}", e.Message);
            }
        }
    }
}
