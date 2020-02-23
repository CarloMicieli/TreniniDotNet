using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugRequest : IRequest
    {
        private readonly Slug _slug;

        public GetScaleBySlugRequest(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}