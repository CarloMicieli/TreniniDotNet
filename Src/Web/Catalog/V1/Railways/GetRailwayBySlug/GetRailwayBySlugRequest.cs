using MediatR;
using TreniniDotNet.Common;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug
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
