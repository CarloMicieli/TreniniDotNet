using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSeeding.Clients.Extensions;
using DataSeeding.DataLoader.Records.Catalog.Brands;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog.Core;
using TreniniDotNet.Catalog;

namespace DataSeeding.Clients.Catalog.Brands
{
    public sealed class BrandsClient
    {
        private BrandsService.BrandsServiceClient Client { get; }
        private Logger Log { get; }

        public BrandsClient(GrpcChannel channel, Logger log)
        {
            Log = log;
            Client = new BrandsService.BrandsServiceClient(channel);
        }

        public async Task<int> SendBrandsAsync(IEnumerable<Brand> brand)
        {
            Log.Debug("Sending brands...");

            var requests = brand.Select(RequestFromRecord);

            try
            {
                var streamingClient = Client.CreateBrands();

                foreach (var request in requests)
                {
                    Log.Debug("Sending {0}", request.Name);
                    await streamingClient.RequestStream.WriteAsync(request);
                }

                await streamingClient.RequestStream.CompleteAsync();

                var response = await streamingClient.ResponseAsync;
                Log.Debug("{0} brand(s) has been created", response.Created);
                return response.Created;
            }
            catch (RpcException e)
            {
                Log.Error(e.Message);
                return 0;
            }
        }

        private CreateBrandRequest RequestFromRecord(Brand brand)
        {
            return new CreateBrandRequest
            {
                Name = brand.Name,
                CompanyName = brand.CompanyName.ToStringOrBlank(),
                Kind = "BrassModels".Equals(brand.BrandKind, StringComparison.InvariantCultureIgnoreCase) ?
                    BrandKind.BrassModels : BrandKind.Industrial,
                WebsiteUrl = brand.WebsiteUrl.ToStringOrBlank(),
                EmailAddress = string.IsNullOrWhiteSpace(brand.MailAddress)
                    ? ""
                    : brand.MailAddress.Replace("[AT]", "@"),
                Description = brand.Description.ToStringOrBlank(),
                GroupName = brand.GroupName.ToStringOrBlank(),
                Address = new Address
                {
                    City = brand.Address?.City.ToStringOrBlank(),
                    Country = brand.Address?.Country.ToStringOrBlank(),
                    PostalCode = brand.Address?.PostalCode.ToStringOrBlank(),
                    Line1 = brand.Address?.Line1.ToStringOrBlank(),
                    Line2 = brand.Address?.Line2.ToStringOrBlank(),
                    Region = brand.Address?.Region.ToStringOrBlank()
                }
            };
        }
    }
}
