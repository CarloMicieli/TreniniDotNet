using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugInput : IUseCaseInput
    {
        public GetCatalogItemBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
