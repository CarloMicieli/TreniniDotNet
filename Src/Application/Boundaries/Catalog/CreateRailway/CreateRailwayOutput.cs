using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateRailway
{
    public sealed class CreateRailwayOutput : IUseCaseOutput
    {
        private readonly Slug _slug;

        public CreateRailwayOutput(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
