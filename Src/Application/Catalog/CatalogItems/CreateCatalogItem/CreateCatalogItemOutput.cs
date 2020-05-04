using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.ValueObjects;

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
