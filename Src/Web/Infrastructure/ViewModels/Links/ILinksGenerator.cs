using TreniniDotNet.Common;
using TreniniDotNet.Common.Pagination;

namespace TreniniDotNet.Web.Infrastructure.ViewModels.Links
{
    public interface ILinksGenerator
    {
        PaginateLinksView Generate<TValue>(string actionName, PaginatedResult<TValue> paginatedResult);

        LinksView GenerateSelfLink(string actionName, Slug slug);
    }
}
