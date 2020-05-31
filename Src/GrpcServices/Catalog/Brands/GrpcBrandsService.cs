using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Extensions;
using BrandKind = TreniniDotNet.Catalog.BrandKind;

namespace TreniniDotNet.GrpcServices.Catalog.Brands
{
    public sealed class GrpcBrandsService : BrandsService.BrandsServiceBase
    {
        public GrpcBrandsService(
            ICreateBrandUseCase useCase,
            CreateBrandPresenter presenter,
            ILogger<GrpcBrandsService> log)
        {
            Log = log ??
                  throw new ArgumentNullException(nameof(log));
            Presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
            UseCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        private ILogger<GrpcBrandsService> Log { get; }
        private CreateBrandPresenter Presenter { get; }
        private ICreateBrandUseCase UseCase { get; }

        public override async Task<CreateBrandResponse> CreateBrand(CreateBrandRequest request, ServerCallContext context)
        {
            var input = InputFromRequest(request);
            await UseCase.Execute(input);
            return Presenter.Response!;
        }

        public override async Task<CreateBrandsResponse> CreateBrands(IAsyncStreamReader<CreateBrandRequest> requestStream, ServerCallContext context)
        {
            var created = 0;

            while (await requestStream.MoveNext())
            {
                var input = InputFromRequest(requestStream.Current);

                try
                {
                    await UseCase.Execute(input);
                    Log.LogInformation("Brand {0} has been created (slug: {1})", input.Name, Presenter.Response.Slug);
                    created++;
                }
                catch (RpcException rpcEx)
                {
                    Log.LogError("Brand name: {0}, error: {1}", input.Name, rpcEx.Message);
                }
            }

            return new CreateBrandsResponse { Created = created };
        }

        private static CreateBrandInput InputFromRequest(CreateBrandRequest request)
        {
            return new CreateBrandInput(
                request.Name.ToNullableString(),
                request.CompanyName.ToNullableString(),
                request.GroupName.ToNullableString(),
                request.Description.ToNullableString(),
                request.WebsiteUrl.ToNullableString(),
                request.EmailAddress.ToNullableString(),
                ToBrandKind(request.Kind),
                ToAddressInput(request.Address));
        }

        private static string ToBrandKind(BrandKind kind) =>
            (kind == BrandKind.BrassModels) ? "BrassModels" : "Industrial";

        private static AddressInput? ToAddressInput(TreniniDotNet.Catalog.Address address)
        {
            if (address is null)
            {
                return null;
            }

            return new AddressInput
            {
                Line1 = address.Line1.ToNullableString(),
                Line2 = address.Line2.ToNullableString(),
                City = address.City.ToNullableString(),
                Country = address.Country.ToNullableString(),
                PostalCode = address.PostalCode.ToNullableString(),
                Region = address.Region.ToNullableString()
            };
        }
    }
}
