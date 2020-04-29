using MediatR;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Web.Collecting.V1.Shops.GetShopsList
{
    public class GetShopsListRequest : IRequest
    {
        public GetShopsListRequest(Page page)
        {
            Page = page;
        }

        public Page Page { get; }
    }
}