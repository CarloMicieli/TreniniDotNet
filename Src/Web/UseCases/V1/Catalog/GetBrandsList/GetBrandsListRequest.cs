using MediatR;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
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
