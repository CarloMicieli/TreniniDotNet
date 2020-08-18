using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

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
