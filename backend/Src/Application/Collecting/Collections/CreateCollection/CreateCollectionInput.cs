using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
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
