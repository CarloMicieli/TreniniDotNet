using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Web.ViewModels.Links
{
    public sealed class AspNetLinksGenerator : ILinksGenerator
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetLinksGenerator(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
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

                nextLink = _linkGenerator.GetUriByAction(httpContext,
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

        public LinksView GenerateSelfLink(string actionName, Slug slug)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var selfLink = _linkGenerator.GetUriByAction(httpContext,
                    action: actionName,
                    values: new { slug = slug.ToString(), version = "1" });

            return new LinksView
            {
                Self = selfLink,
                Slug = slug.ToString()
            };
        }
    }
}
