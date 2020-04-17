using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public sealed class GetCollectionByOwner : ValidatedUseCase<GetCollectionByOwnerInput, IGetCollectionByOwnerOutputPort>, IGetCollectionByOwnerUseCase
    {
        public GetCollectionByOwner(IGetCollectionByOwnerOutputPort output)
            : base(new GetCollectionByOwnerInputValidator(), output)
        {
        }

        protected override Task Handle(GetCollectionByOwnerInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
