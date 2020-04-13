using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class AddItemToCollection : ValidatedUseCase<AddItemToCollectionInput, IAddItemToCollectionOutputPort>, IAddItemToCollectionUseCase
    {
        public AddItemToCollection(IAddItemToCollectionOutputPort output)
            : base(new AddItemToCollectionInputValidator(), output)
        {
        }

        //TODO: add a new catalog item to this collection 
        //      it should be possible to add the same item more than once
        //      (in case of cars, it is possible to have the same item more than once)
        protected override Task Handle(AddItemToCollectionInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
