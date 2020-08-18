using MediatR;
using TreniniDotNet.Common.Data.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Brands.GetBrandsList
{
    public sealed class GetBrandsListRequest : IRequest
    {
        public GetBrandsListRequest(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}
