using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class EditCollectionItem : ValidatedUseCase<EditCollectionItemInput, IEditCollectionItemOutputPort>, IEditCollectionItemUseCase
    {
        public EditCollectionItem(IEditCollectionItemOutputPort output)
            : base(new EditCollectionItemInputValidator(), output)
        {
        }

        protected override Task Handle(EditCollectionItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
