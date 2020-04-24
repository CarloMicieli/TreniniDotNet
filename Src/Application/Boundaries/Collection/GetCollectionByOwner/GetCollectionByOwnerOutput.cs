using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerOutput : IUseCaseOutput
    {
        public GetCollectionByOwnerOutput(ICollection collection)
        {
            Collection = collection;
        }

        public ICollection Collection { get; }
    }
}
