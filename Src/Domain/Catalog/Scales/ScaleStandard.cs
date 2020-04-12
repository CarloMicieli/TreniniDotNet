using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public enum ScaleStandard
    {
        // NEM-standards are used by model railway industry and hobbyists in Europe.
        NEM,

        // NMRA standards are used widely in North America and by certain special 
        // interest groups all over the world.
        NRMA,

        British,

        Japanese
    }

    public static class ScaleStandards
    {
        public static bool TryParse(this string? str, out ScaleStandard? result)
        {
            if (Enum.TryParse<ScaleStandard>(str, out var standard))
            {
                result = standard;
                return true;
            }

            result = default;
            return false;
        }

        public static ScaleStandard Parse(this string str)
        {
            if (Enum.TryParse<ScaleStandard>(str, out var standard))
            {
                return standard;
            }

            throw new ArgumentOutOfRangeException(nameof(str));
        }

        public static IImmutableSet<ScaleStandard> ToSet(IEnumerable<string> values)
        {
            return values == null ?
                ImmutableSortedSet.Create<ScaleStandard>() :
                values.Select(it => ScaleStandards.Parse(it))
                    .ToImmutableSortedSet();
        }
    }
}