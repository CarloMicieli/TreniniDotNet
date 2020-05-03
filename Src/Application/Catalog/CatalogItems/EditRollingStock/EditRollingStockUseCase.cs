using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockUseCase : ValidatedUseCase<EditRollingStockInput, IEditRollingStockOutputPort>, IEditRollingStockUseCase
    {
        public EditRollingStockUseCase(IEditRollingStockOutputPort output)
            : base(new EditRollingStockInputValidator(), output)
        {
        }

        protected override Task Handle(EditRollingStockInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
