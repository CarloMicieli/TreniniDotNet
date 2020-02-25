using System;

namespace TreniniDotNet.Domain.Pagination
{
    public readonly struct Page
    {
        public Page(int start, int limit)
        { 
            this.Limit = limit;
            this.Start = start;
        }

        public Page Next()
        {
            return new Page(Limit + Start, Limit);
        }

        public Page Prev()
        {
            int newStart = Start > Limit ? Start - Limit : 0;
            return new Page(newStart, Limit);
        }

        public int Limit { get; }

        public int Start { get; }

        public override bool Equals(object? obj)
        {
            return obj is Page page &&
                   Limit == page.Limit &&
                   Start == page.Start;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Limit, Start);
        }

        public static bool operator ==(Page left, Page right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Page left, Page right)
        {
            return !(left == right);
        }
    }
}
