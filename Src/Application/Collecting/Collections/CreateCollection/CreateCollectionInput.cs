using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

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
