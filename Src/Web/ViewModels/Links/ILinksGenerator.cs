using TreniniDotNet.Common;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.ViewModels.Links
{
    public interface ILinksGenerator
    {
        PaginateLinksView Generate<TValue>(string actionName, PaginatedResult<TValue> paginatedResult);

        LinksView GenerateSelfLink(string actionName, Slug slug);
    }
}
