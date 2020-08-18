using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Web.Infrastructure.ViewModels.Links
{
    public interface ILinksGenerator
    {
        PaginateLinksView Generate<TValue>(string actionName, PaginatedResult<TValue> paginatedResult);

        LinksView GenerateSelfLink(string actionName, Slug slug);
    }
}
