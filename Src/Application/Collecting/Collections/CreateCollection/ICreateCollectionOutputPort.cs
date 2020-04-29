using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public interface ICreateCollectionOutputPort : IOutputPortStandard<CreateCollectionOutput>
    {
        void UserHasAlreadyOneCollection(string message);
    }
}
