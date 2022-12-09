namespace AoCUtils;

public static class Parsing
{
    /// <summary>
    /// Totally, 110%, terribly unsafe parser. But it's fast!
    /// </summary>
    /// <param name="input">String to parse into an integer</param>
    /// <returns>The integer</returns>
    public static int FastIntParse(string input)
    {
        var parseValue = 0;
        foreach (var c in input)
            parseValue = parseValue * 10 + (c - '0');

        return parseValue;
    }
    
    public static int FastIntParse(ReadOnlySpan<char> input)
    {
        var parseValue = 0;
        foreach (var c in input)
            parseValue = parseValue * 10 + (c - '0');

        return parseValue;
    }
}
