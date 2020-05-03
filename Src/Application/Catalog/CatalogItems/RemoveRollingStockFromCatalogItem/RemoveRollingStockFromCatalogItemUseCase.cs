using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public sealed class RemoveRollingStockFromCatalogItemUseCase : ValidatedUseCase<RemoveRollingStockFromCatalogItemInput, IRemoveRollingStockFromCatalogItemOutputPort>, IRemoveRollingStockFromCatalogItemUseCase
    {
        public RemoveRollingStockFromCatalogItemUseCase(IRemoveRollingStockFromCatalogItemOutputPort output)
            : base(new RemoveRollingStockFromCatalogItemInputValidator(), output)
        {
        }

        protected override Task Handle(RemoveRollingStockFromCatalogItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
