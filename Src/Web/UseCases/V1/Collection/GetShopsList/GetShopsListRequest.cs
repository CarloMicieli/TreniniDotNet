using MediatR;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetShopsList
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