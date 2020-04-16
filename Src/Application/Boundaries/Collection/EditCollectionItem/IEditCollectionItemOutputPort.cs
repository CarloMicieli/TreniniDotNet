namespace TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem
{
    public interface IEditCollectionItemOutputPort : IOutputPortStandard<EditCollectionItemOutput>
    {
        void CollectionNotFound(string message);

        void CollectionItemNotFound(string message);

        void ShopNotFound(string message);
    }
}
