using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public sealed class CreateCollectionInput : IUseCaseInput
    {
        public CreateCollectionInput(string owner, string? notes)
        {
            Owner = owner;
            Notes = notes;
        }

        public string Owner { get; }

        public string? Notes { get; }
    }
}
