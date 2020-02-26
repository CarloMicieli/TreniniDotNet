using MediatR;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
{
    public sealed class GetScalesListRequest : IRequest
    {
        public GetScalesListRequest(Page? page)
        {
            Page = page;
        }

        public Page? Page { get; }
    }
}
