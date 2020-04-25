using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;

namespace TreniniDotNet.Application.UseCases.Catalog.CatalogItems
{
    public sealed class EditCatalogItem : ValidatedUseCase<EditCatalogItemInput, IEditCatalogItemOutputPort>, IEditCatalogItemUseCase
    {
        public EditCatalogItem(IEditCatalogItemOutputPort output)
            : base(new EditCatalogItemInputValidator(), output)
        {
        }

        protected override Task Handle(EditCatalogItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
