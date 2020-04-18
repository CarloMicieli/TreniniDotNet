using System.Collections.Generic;

namespace TreniniDotNet.TestHelpers.SeedData
{
    public static class ListHelpers
    {
        public static IEnumerable<TValue> ListOf<TValue>(params TValue[] elements)
        {
            return new List<TValue>(elements);
        }
    }
}
