using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug
{
    public class GetRailwayBySlugRequest : IRequest
    {
        private readonly Slug _slug;

        public GetRailwayBySlugRequest(Slug slug)
        {
            _slug = slug;
        }

        public Slug Slug => _slug;
    }
}
