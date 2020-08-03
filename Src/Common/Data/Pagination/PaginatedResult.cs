using System;
using System.Collections.Generic;
using System.Linq;

namespace TreniniDotNet.Common.Data.Pagination
{
    public sealed class PaginatedResult<TValue> : IEquatable<PaginatedResult<TValue>>
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

        public override int GetHashCode() => HashCode.Combine(CurrentPage, Results);

        public static bool operator ==(PaginatedResult<TValue> left, PaginatedResult<TValue> right) =>
            AreEquals(left, right);

        public static bool operator !=(PaginatedResult<TValue> left, PaginatedResult<TValue> right) =>
            !AreEquals(left, right);

        public override bool Equals(object? obj)
        {
            if (obj is PaginatedResult<TValue> that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(PaginatedResult<TValue>? other) =>
            AreEquals(this, other);

        private static bool AreEquals<T>(PaginatedResult<T> left, PaginatedResult<T>? right) =>
            left.CurrentPage == right?.CurrentPage &&
            left.Results.Equals(right.Results);
    }
}
