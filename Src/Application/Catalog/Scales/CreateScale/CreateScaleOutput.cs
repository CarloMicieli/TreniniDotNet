using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public class CreateScaleOutput : IUseCaseOutput
    {
        public CreateScaleOutput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
