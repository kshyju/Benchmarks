namespace Benchmarks.ConsoleApp;

public static class StringUtils
{
    public static HashSet<string> CreateHashSetUsingStringSplit (string input)
    {
        return input.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    public static HashSet<string> CreateHashSetOptimized(string input)
    {
        var s = input.AsSpan();
        int cap = 1;
        for (int k = 0; k < s.Length; k++)
        {
            if (s[k] == '|') cap++;
        }

        var set = new HashSet<string>(cap, StringComparer.OrdinalIgnoreCase);

        int i = 0;
        while (i <= s.Length)
        {
            int j = s[i..].IndexOf('|');
            ReadOnlySpan<char> part;
            if (j < 0)
            {
                part = s[i..];          
                i = s.Length + 1;      
            }
            else
            {
                part = s.Slice(i, j);
                i += j + 1;
            }

            part = TrimWhitespace(part);

            if (!part.IsEmpty)
            {
                set.Add(part.ToString()); 
            }
        }

        return set;
    }

    private static ReadOnlySpan<char> TrimWhitespace(ReadOnlySpan<char> span)
    {
        int start = 0, end = span.Length - 1;

        while (start <= end && char.IsWhiteSpace(span[start])) start++;
        while (end >= start && char.IsWhiteSpace(span[end])) end--;

        return start > end ? [] : span.Slice(start, end - start + 1);
    }
}