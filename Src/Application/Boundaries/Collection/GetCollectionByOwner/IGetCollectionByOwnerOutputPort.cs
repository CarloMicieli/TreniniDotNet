using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
{
    public interface IGetCollectionByOwnerOutputPort : IOutputPortStandard<GetCollectionByOwnerOutput>
    {
        void CollectionNotFound(Owner owner);
    }
}
