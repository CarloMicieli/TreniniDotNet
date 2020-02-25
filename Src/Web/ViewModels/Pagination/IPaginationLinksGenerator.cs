using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.Pagination
{
    public interface IPaginationLinksGenerator
    {
        PaginateLinksView Generate<TValue>(string actionName, PaginatedResult<TValue> paginatedResult);

        string GenerateLink(string actionName, object values);
    }
}