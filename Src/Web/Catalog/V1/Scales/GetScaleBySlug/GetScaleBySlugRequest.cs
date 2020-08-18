using MediatR;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScaleBySlug
{
    public class GetScaleBySlugRequest : IRequest
    {
        public GetScaleBySlugRequest(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}