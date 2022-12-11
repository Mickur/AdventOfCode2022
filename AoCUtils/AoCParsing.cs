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
            if (input[index] == ' ')
                continue;
            
            parseValue = parseValue * 10 + (input[index] - '0');
        }

        return isNegative ? 0 - parseValue : parseValue;
    }
    
    public static ulong FastULongParse(string input)
    {
        return FastULongParse(input.AsSpan());
    }
    
    public static ulong FastULongParse(ReadOnlySpan<char> input)
    {
        ulong parseValue = 0;
        var index = 0;
        
        for (; index < input.Length; index++)
        {
            if (input[index] == ' ')
                continue;
            
            parseValue = parseValue * 10 + (ulong)(input[index] - '0');
        }

        return parseValue;
    }
}
