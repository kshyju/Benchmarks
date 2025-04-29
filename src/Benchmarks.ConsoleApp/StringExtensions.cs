namespace Benchmarks.ConsoleApp
{
    public static class StringExtensions
    {
        public static bool ContainsDelimitedValue(
            this string input,
            string target,
            char delimiter = ',',
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            ReadOnlySpan<char> span = input.AsSpan();
            ReadOnlySpan<char> targetSpan = target.AsSpan();

            while (!span.IsEmpty)
            {
                int delimiterIndex = span.IndexOf(delimiter);
                ReadOnlySpan<char> token;

                if (delimiterIndex >= 0)
                {
                    token = span.Slice(0, delimiterIndex).Trim();
                    span = span.Slice(delimiterIndex + 1);
                }
                else
                {
                    token = span.Trim();
                    span = default;
                }

                if (token.Equals(targetSpan, comparison))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
