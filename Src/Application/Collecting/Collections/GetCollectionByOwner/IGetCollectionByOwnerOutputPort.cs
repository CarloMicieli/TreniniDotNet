using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public interface IGetCollectionByOwnerOutputPort : IOutputPortStandard<GetCollectionByOwnerOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
