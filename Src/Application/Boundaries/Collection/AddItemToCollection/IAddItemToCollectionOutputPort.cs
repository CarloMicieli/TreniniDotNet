namespace TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection
{
    public interface IAddItemToCollectionOutputPort : IOutputPortStandard<AddItemToCollectionOutput>
    {
        void CollectionNotFound(string message);

        void ShopNotFound(string message);
    }
}
