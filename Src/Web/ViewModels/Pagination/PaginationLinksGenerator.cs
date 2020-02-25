using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.Pagination
{
    public sealed class PaginationLinksGenerator : IPaginationLinksGenerator
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaginationLinksGenerator(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateLink(string actionName, object values)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return _linkGenerator.GetUriByAction(httpContext,
                    action: actionName,
                    values: values);
        }

        public PaginateLinksView Generate<TValue>(string actionName, PaginatedResult<TValue> paginatedResult)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            string selfLink = _linkGenerator.GetUriByAction(httpContext,
                    action: actionName,
                    values: new { start = paginatedResult.CurrentPage.Start, limit = paginatedResult.CurrentPage.Limit });

            string? prevLink = null;
            if (paginatedResult.HasPrevious)
            {
                Page prevPage = paginatedResult.CurrentPage.Prev();

                prevLink = _linkGenerator.GetUriByAction(httpContext,
                    action: actionName,
                    values: new { start = prevPage.Start, limit = prevPage.Limit });
            }

            string? nextLink = null;
            if (paginatedResult.HasNext)
            {
                Page nextPage = paginatedResult.CurrentPage.Next();

                prevLink = _linkGenerator.GetUriByAction(httpContext,
                    action: actionName,
                    values: new { start = nextPage.Start, limit = nextPage.Limit });
            }

            return new PaginateLinksView
            {
                Self = selfLink,
                Next = nextLink,
                Prev = prevLink
            };
        }
    }
}
