using System.Text.RegularExpressions;

namespace Plumbing.Extensions
{
    public static class RegExHelpers
    {
        public static bool Matches(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsInteger(this string input)
        {
            return input.Matches(@"^\d+$");
        }

        public static bool Is8DigitInteger(this string input)
        {
            return input.Matches(@"^\d{8}$");
        }
    }
}