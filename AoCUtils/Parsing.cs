namespace AoCUtils;

public static class AoCParsing
{
    public static int FastIntParse(string input)
    {
        return FastIntParse(input.AsSpan());
    }
    
    public static int FastIntParse(ReadOnlySpan<char> input)
    {
        var isNegative = false;
        var parseValue = 0;
        var index = 0;
        
        // Handle negative numbers
        if (input[0] == '-')
        {
            isNegative = true;
            index = 1;
        }
        
        for (; index < input.Length; index++)
        {
            parseValue = parseValue * 10 + (input[index] - '0');
        }

        return isNegative ? 0 - parseValue : parseValue;
    }
}
