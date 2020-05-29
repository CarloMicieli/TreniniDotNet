using System;
using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.GrpcServices.Infrastructure;

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
                Slug = output.Slug,
                CatalogItemId = output.Id.ToString()
            };

        public void BrandNotFound(Slug brand)
        {
            throw new NotImplementedException();
        }

        public void CatalogItemAlreadyExists(IBrandInfo brand, ItemNumber itemNumber)
        {
            throw new NotImplementedException();
        }

        public void ScaleNotFound(Slug scale)
        {
            throw new NotImplementedException();
        }

        public void RailwayNotFound(IEnumerable<Slug> railways)
        {
            throw new NotImplementedException();
        }
    }
}
