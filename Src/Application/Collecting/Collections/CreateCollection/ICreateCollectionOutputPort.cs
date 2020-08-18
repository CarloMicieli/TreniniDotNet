using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public interface ICreateCollectionOutputPort : IStandardOutputPort<CreateCollectionOutput>
    {
        void UserHasAlreadyOneCollection(Owner owner);
    }
}
