using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public class GetScaleBySlugInput : IUseCaseInput
    {
        public GetScaleBySlugInput(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}