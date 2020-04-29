using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

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