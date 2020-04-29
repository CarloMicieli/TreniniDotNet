using System;

namespace TreniniDotNet.Common.Pagination
{
    public readonly struct Page : IEquatable<Page>
    {
        public Page(int start, int limit)
        {
            if (start < 0)
            {
                throw new ArgumentException($"Invalid page: {start} is not valid, it must be positive", nameof(start));
            }

            if (limit < 0)
            {
                throw new ArgumentException($"Invalid page: {limit} is not valid, it must be positive", nameof(limit));
            }

            this.Limit = limit;
            this.Start = start;
        }

        public int Limit { get; }

        public int Start { get; }

        public static Page Default => new Page(0, 50);

        public Page Next()
        {
            return new Page(Limit + Start, Limit);
        }

        public Page Prev()
        {
            int newStart = Start > Limit ? Start - Limit : 0;
            return new Page(newStart, Limit);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Page that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Page other) => AreEquals(this, other);

        public static bool operator ==(Page left, Page right) => AreEquals(left, right);

        public static bool operator !=(Page left, Page right) => !AreEquals(left, right);

        private static bool AreEquals(Page left, Page right) =>
            left.Limit == right.Limit &&
            left.Start == right.Start;

        public override int GetHashCode() => HashCode.Combine(Limit, Start);

        public override string ToString() => $"Page(start: {Start}, limit: {Limit})";
    }
}
