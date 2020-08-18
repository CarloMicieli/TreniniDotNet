using System.Text;
using System.Text.RegularExpressions;

namespace TreniniDotNet.SharedKernel.Slugs
{
    public static class StringToSeoFriendlyExtensions
    {
        public static string ToSeoFriendly(this string str)
        {
            return ToSeoFriendly(str, str.Length);
        }

        public static string ToSeoFriendly(this string str, int maxLength)
        {
            var match = Regex.Match(str.ToLower(), "[\\w]+");
            StringBuilder result = new StringBuilder("");
            bool maxLengthHit = false;
            while (match.Success && !maxLengthHit)
            {
                if (result.Length + match.Value.Length <= maxLength)
                {
                    result.Append(match.Value + "-");
                }
                else
                {
                    maxLengthHit = true;
                    // Handle a situation where there is only one word and it is greater than the max length.  
                    if (result.Length == 0)
                    {
                        result.Append(match.Value.Substring(0, maxLength));
                    }
                }
                match = match.NextMatch();
            }
            // Remove trailing '-'  
            if (result[^1] == '-')
            {
                result.Remove(result.Length - 1, 1);
            }
            return result.ToString();
        }
    }
}