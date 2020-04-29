using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public sealed class CreateRailwayOutput : IUseCaseOutput
    {
        public CreateRailwayOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
