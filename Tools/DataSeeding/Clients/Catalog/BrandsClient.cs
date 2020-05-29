using System;
using System.Threading.Tasks;
using DataSeeding.DataLoader.Records.Catalog;
using DataSeeding.DataLoader.Records.Catalog.Brands;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog.Core;
using TreniniDotNet.Catalog;

namespace DataSeeding.Clients.Catalog
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

        public async Task NewBrandAsync(Brand brand)
        {
            var request = new CreateBrandRequest
            {
                Name = brand.Name,
                CompanyName = brand.CompanyName ?? "",
                Kind = "BrassModels".Equals(brand.BrandKind, StringComparison.InvariantCultureIgnoreCase) ?
                    BrandKind.BrassModels : BrandKind.Industrial,
                WebsiteUrl = brand.WebsiteUrl ?? "",
                EmailAddress = string.IsNullOrWhiteSpace(brand.MailAddress)
                    ? null
                    : brand.MailAddress.Replace("[AT]", "@"),
                Description = brand.Description ?? "",
                GroupName = brand.GroupName ?? "",
                Address = new Address
                {
                    City = brand.Address?.City ?? "",
                    Country = brand.Address?.Country ?? "",
                    PostalCode = brand.Address?.PostalCode ?? "",
                    Line1 = brand.Address?.Line1 ?? "",
                    Line2 = brand.Address?.Line2 ?? "",
                    Region = brand.Address?.Region ?? ""
                }
            };

            Log.Debug("Sending brand : {0}", request);

            try
            {
                var response = await Client.CreateBrandAsync(request);
                Log.Debug("Response was: {0}", response);
            }
            catch (RpcException e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
