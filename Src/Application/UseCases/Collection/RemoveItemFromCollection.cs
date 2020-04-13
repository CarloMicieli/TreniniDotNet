using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class RemoveItemFromCollection : ValidatedUseCase<RemoveItemFromCollectionInput, IRemoveItemFromCollectionOutputPort>, IRemoveItemFromCollectionUseCase
    {
        public RemoveItemFromCollection(IRemoveItemFromCollectionOutputPort output)
            : base(new RemoveItemFromCollectionInputValidator(), output)
        {
        }

        protected override Task Handle(RemoveItemFromCollectionInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
