namespace Benchmarks.ConsoleApp;

public static class StringUtils
{
    public static bool ContainsUsingStringSplit(string featureFlags, string tokenToSearchFor, char separator = ',')
    {
        if (!string.IsNullOrEmpty(featureFlags))
        {
            return featureFlags.Split(separator).Contains(tokenToSearchFor, StringComparer.OrdinalIgnoreCase);
        }

        return false;
    }

    public static bool ContainsToken(string delimitedString, string searchToken, char separator = ',', StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
    {
        if (string.IsNullOrEmpty(delimitedString) || string.IsNullOrEmpty(searchToken))
        {
            return false;
        }

        var remaining = delimitedString.AsSpan();
        var searchSpan = searchToken.AsSpan();

        while (!remaining.IsEmpty)
        {
            int separatorIndex = remaining.IndexOf(separator);
            ReadOnlySpan<char> currentToken;

            if (separatorIndex >= 0)
            {
                currentToken = remaining.Slice(0, separatorIndex);
                remaining = remaining.Slice(separatorIndex + 1);
            }
            else
            {
                currentToken = remaining;
                remaining = default;
            }

            if (currentToken.Equals(searchSpan, comparisonType))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ContainsTokenWithValidation(string delimitedString, string searchToken, char separator = ',', StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
    {
        if (string.IsNullOrEmpty(delimitedString) || string.IsNullOrEmpty(searchToken))
        {
            return false;
        }

        if (searchToken.Contains(separator))
        {
            throw new ArgumentException($"The search token must not contain the separator character '{separator}'.", nameof(searchToken));
        }

        var remaining = delimitedString.AsSpan();
        var searchSpan = searchToken.AsSpan();

        while (!remaining.IsEmpty)
        {
            int separatorIndex = remaining.IndexOf(separator);
            ReadOnlySpan<char> currentToken;

            if (separatorIndex >= 0)
            {
                currentToken = remaining.Slice(0, separatorIndex);
                remaining = remaining.Slice(separatorIndex + 1);
            }
            else
            {
                currentToken = remaining;
                remaining = default;
            }

            if (currentToken.Equals(searchSpan, comparisonType))
            {
                return true;
            }
        }

        return false;
    }
}