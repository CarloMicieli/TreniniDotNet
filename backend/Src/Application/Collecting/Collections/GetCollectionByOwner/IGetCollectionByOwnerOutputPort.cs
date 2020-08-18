using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public interface IGetCollectionByOwnerOutputPort : IStandardOutputPort<GetCollectionByOwnerOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
