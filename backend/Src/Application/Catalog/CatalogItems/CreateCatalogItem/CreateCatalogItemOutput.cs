using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public sealed class CreateCatalogItemOutput : IUseCaseOutput
    {
        public CreateCatalogItemOutput(CatalogItemId id, Slug slug)
        {
            Id = id;
            Slug = slug;
        }

        public CatalogItemId Id { get; }

        public Slug Slug { get; }
    }
}
