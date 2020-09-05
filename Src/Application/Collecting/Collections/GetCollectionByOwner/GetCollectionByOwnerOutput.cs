using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerOutput : IUseCaseOutput
    {
        public GetCollectionByOwnerOutput(Collection collection)
        {
            Collection = collection;
        }

        public Collection Collection { get; }
    }
}
