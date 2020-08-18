using System.Net.Http;
using DataSeeding.Clients.Catalog;
using DataSeeding.Clients.Catalog.Brands;
using DataSeeding.Clients.Catalog.CatalogItems;
using DataSeeding.Clients.Catalog.Railways;
using DataSeeding.Clients.Catalog.Scales;
using Grpc.Net.Client;
using Serilog.Core;

namespace DataSeeding.Clients
{
    public class CatalogClient
    {
        private Logger Log { get; }

        public BrandsClient Brands { get; }

        public RailwaysClient Railways { get; }

        public ScalesClient Scales { get; }

        public CatalogItemsClient CatalogItemsClient { get; }

        public CatalogClient(string baseAddress, Logger log)
        {
            Log = log;

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress(baseAddress, new GrpcChannelOptions { HttpClient = httpClient });

            Brands = new BrandsClient(channel, log);
            CatalogItemsClient = new CatalogItemsClient(channel, log);
            Railways = new RailwaysClient(channel, log);
            Scales = new ScalesClient(channel, log);
        }
    }
}
