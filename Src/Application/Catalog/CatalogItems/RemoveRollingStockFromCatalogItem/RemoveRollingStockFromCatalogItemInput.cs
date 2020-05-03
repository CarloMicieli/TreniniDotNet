using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Input;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemInput : IUseCaseInput
    {
        public RemoveRollingStockFromCatalogItemInput(Slug slug, RollingStockId id)
        {
            CatalogItemSlug = slug;
            RollingStockId = id;
        }

        public Slug CatalogItemSlug { get; }
        public RollingStockId RollingStockId { get; }
    }
}
