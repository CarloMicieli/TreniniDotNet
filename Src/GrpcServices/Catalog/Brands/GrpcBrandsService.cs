using System;
using System.Threading.Tasks;
using Grpc.Core;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Brands.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.GrpcServices.Extensions;
using BrandKind = TreniniDotNet.Catalog.BrandKind;

namespace TreniniDotNet.GrpcServices.Catalog.Brands
{
    public sealed class GrpcBrandsService : BrandsService.BrandsServiceBase
    {
        public GrpcBrandsService(
            BrandService brandService,
            IUnitOfWork unitOfWork,
            CreateBrandPresenter presenter)
        {
            Presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
            UseCase = new CreateBrandUseCase(presenter, brandService, unitOfWork);
        }

        private CreateBrandPresenter Presenter { get; }
        private ICreateBrandUseCase UseCase { get; }

        public override async Task<CreateBrandResponse> CreateBrand(CreateBrandRequest request, ServerCallContext context)
        {
            var input = new CreateBrandInput(
                request.Name.ToNullableString(),
                request.CompanyName.ToNullableString(),
                request.GroupName.ToNullableString(),
                request.Description.ToNullableString(),
                request.WebsiteUrl.ToNullableString(),
                request.EmailAddress.ToNullableString(),
                ToBrandKind(request.Kind),
                ToAddressInput(request.Address));

            await UseCase.Execute(input);
            return Presenter.Response!;
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
