using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerInput : IUseCaseInput
    {
        public GetCollectionByOwnerInput(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; }
    }
}
