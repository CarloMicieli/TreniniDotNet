using System;

namespace TreniniDotNet.Common.Enums
{
    public static class EnumHelpers
    {
        // Try to parse the string as one of the constant names of the Enum,
        // return null if no such constant was found
        public static TEnum? OptionalValueFor<TEnum>(string? str)
            where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(str) == false &&
                Enum.TryParse<TEnum>(str, true, out var c))
            {
                return c;
            }

            return null;
        }

        // Parse the string as one of the Enum constant,
        // will throw an exception for not found constants.
        public static TEnum RequiredValueFor<TEnum>(string str)
            where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(str, true, out var result))
            {
                return result;
            }

            string typeName = typeof(TEnum).Name;
            throw new ArgumentException($"The value '{str}' was not a valid constant for {typeName}.");
        }
    }
}
