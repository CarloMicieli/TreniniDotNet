using System.Collections.Generic;
using System.Linq;

namespace TreniniDotNet.Domain.Pagination
{
    public sealed class PaginatedResult<TValue>
    {
        public PaginatedResult(Page currentPage, IEnumerable<TValue> results)
        {
            CurrentPage = currentPage;
            Results = results.Take(currentPage.Limit);

            HasPrevious = currentPage.Start > 0;
            HasNext = results.Count() > currentPage.Limit;
        }

        public Page CurrentPage { get; }

        public bool HasPrevious { get; }

        public bool HasNext { get; }

        public IEnumerable<TValue> Results { get; }

        public Page? Next()
        {
            return HasNext ? CurrentPage.Next() : default;
        }

        public Page? Previous()
        {
            return HasPrevious ? CurrentPage.Prev() : default;
        }
    }
}
