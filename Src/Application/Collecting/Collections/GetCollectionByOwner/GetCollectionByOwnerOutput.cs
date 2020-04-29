using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
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
