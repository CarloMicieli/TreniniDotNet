using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemOutput : IUseCaseOutput
    {
        private readonly CatalogItemId _id;
        private readonly Slug _slug;

        public CreateCatalogItemOutput(CatalogItemId id, Slug slug)
        {
            _id = id;
            _slug = slug;
        }

        public CatalogItemId Id => _id;

        public Slug Slug => _slug;
    }
}
