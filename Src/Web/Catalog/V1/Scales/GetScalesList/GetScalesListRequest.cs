using MediatR;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Web.Catalog.V1.Scales.GetScalesList
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
