namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public interface ICreateCollectionOutputPort : IOutputPortStandard<CreateCollectionOutput>
    {
        void UserHasAlreadyOneCollection(string message);
    }
}
