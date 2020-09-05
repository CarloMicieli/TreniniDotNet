using MediatR;
using TreniniDotNet.Common;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug
{
    public class GetRailwayBySlugRequest : IRequest
    {
        public GetRailwayBySlugRequest(Slug slug)
        {
            Slug = slug;
        }

        public Slug Slug { get; }
    }
}
