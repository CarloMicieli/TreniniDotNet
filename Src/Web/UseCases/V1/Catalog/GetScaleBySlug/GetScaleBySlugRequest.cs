using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
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