using System;
using System.Linq;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.Pagination
{
    public static class PaginatedResultExtensions
    {
        public static PaginatedViewModel<TViewModel> ToViewModel<TValue, TViewModel>(
            this PaginatedResult<TValue> paginatedResult,
            PaginateLinksView links,
            Func<TValue, TViewModel> mapping)
        {
            return new PaginatedViewModel<TViewModel>
            {
                Links = links,
                Limit = paginatedResult.CurrentPage.Limit,
                Results = paginatedResult.Results.Select(mapping).ToList()
            };
        }
    }
}
