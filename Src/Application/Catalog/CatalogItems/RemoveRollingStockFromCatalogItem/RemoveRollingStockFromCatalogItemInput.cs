using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Input;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemInput : IUseCaseInput
    {
        public RemoveRollingStockFromCatalogItemInput(Slug slug, RollingStockId rollingStockId)
        {
            CatalogItemSlug = slug;
            RollingStockId = rollingStockId;
        }

        public Slug CatalogItemSlug { get; }
        public RollingStockId RollingStockId { get; }
    }
}
