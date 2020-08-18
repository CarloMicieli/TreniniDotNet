using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Catalog;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.GrpcServices.Infrastructure;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.GrpcServices.Catalog.CatalogItems
{
    public sealed class CreateCatalogItemPresenter : DefaultGrpcPresenter<CreateCatalogItemOutput, CreateCatalogItemResponse>, ICreateCatalogItemOutputPort
    {
        public CreateCatalogItemPresenter()
            : base(Mapping)
        {
        }

        private static CreateCatalogItemResponse Mapping(CreateCatalogItemOutput output) =>
            new CreateCatalogItemResponse
            {
                Slug = output.Slug
            };

        public void BrandNotFound(Slug brand)
        {
            NotFound($"Brand {brand} was not found");
        }

        public void CatalogItemAlreadyExists(Brand brand, ItemNumber itemNumber)
        {
            AlreadyExists($"Catalog item {brand} {itemNumber} already exist");
        }

        public void ScaleNotFound(Slug scale)
        {
            NotFound($"Scale {scale} was not found");
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            NotFound($"Railway(s) [{string.Join(", ", railways)}] not found");
        }
    }
}
