using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetUserCollection;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class GetUserCollection : ValidatedUseCase<GetUserCollectionInput, IGetUserCollectionOutputPort>, IGetUserCollectionUseCase
    {
        public GetUserCollection(IGetUserCollectionOutputPort output)
            : base(new GetUserCollectionInputValidator(), output)
        {
        }

        protected override Task Handle(GetUserCollectionInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
