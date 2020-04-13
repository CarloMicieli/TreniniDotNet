using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class CreateCollection : ValidatedUseCase<CreateCollectionInput, ICreateCollectionOutputPort>, ICreateCollectionUseCase
    {
        public CreateCollection(ICreateCollectionOutputPort output)
            : base(new CreateCollectionInputValidator(), output)
        {
        }

        protected override Task Handle(CreateCollectionInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
