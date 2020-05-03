using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Catalog.CatalogItems.AddRollingStockToCatalogItem
{
    public sealed class AddRollingStockToCatalogItemUseCase : ValidatedUseCase<AddRollingStockToCatalogItemInput, IAddRollingStockToCatalogItemOutputPort>, IAddRollingStockToCatalogItemUseCase
    {
        public AddRollingStockToCatalogItemUseCase(IAddRollingStockToCatalogItemOutputPort output)
            : base(new AddRollingStockToCatalogItemInputValidator(), output)
        {
        }

        protected override Task Handle(AddRollingStockToCatalogItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
