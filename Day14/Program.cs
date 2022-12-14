using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 14!");

// Setup
var input = File.ReadAllLines("input.txt");

var slice = new Derp(input);
slice.DrawSlice();


class Derp
{
    private Dictionary<(int, int), char> slice = new ();

    private int minX = 500;
    private int maxX = 500;
    private int minY = 0;
    private int maxY = 0;

    public Derp(string[] input)
    {
        foreach (var line in input)
        {
            var steps = line.Split(" -> ");

            var previous = steps[0].Split(',');
            var previousX = AoCParsing.FastIntParse(previous[0]);
            var previousY = AoCParsing.FastIntParse(previous[1]);
            
            slice.Add((previousX, previousY), '#');
            
            if (previousX < minX)
                minX = previousX;
            else if (previousX > maxX)
                maxX = previousX;
                
            if (previousY < minY)
                minY = previousY;
            else if (previousY > maxY)
                maxY = previousY;
            
            for(var i = 1; i < steps.Length; i++)
            {
                var coords = steps[i].Split(',');
                var x = AoCParsing.FastIntParse(coords[0]);
                var y = AoCParsing.FastIntParse(coords[1]);
                
                if (x < minX)
                    minX = x;
                else if (x > maxX)
                    maxX = x;
                
                if (y < minY)
                    minY = y;
                else if (y > maxY)
                    maxY = y;

                // Same X position
                if (x == previousX)
                {
                    if (y < previousY)
                    {
                        for (var a = previousY - 1; a >= y; a--)
                        {
                            slice.Add((x, a), '#');
                            previousY = a;
                        }
                    }
                    else if(y > previousY)
                    {
                        for (var a = previousY + 1; a <= y; a++)
                        {
                            slice.Add((x, a), '#');
                            previousY = a;
                        }
                    }
                }
                
                // Iterate Y
                else if (y == previousY)
                {
                    if (x < previousX)
                    {
                        for (var a = previousX - 1; a >= x; a--)
                        {
                            slice.Add((a, y), '#');
                            previousX = a;
                        }
                    }
                    else if(x > previousX)
                    {
                        for (var a = previousX + 1; a <= x; a++)
                        {
                            slice.Add((a, y), '#');
                            previousX = a;
                        }
                    }
                }
            }
        }
    }

    public void DrawSlice()
    {
        for (var y = minY - 1; y <= maxY + 1; y++)
        {
            for (var x = minX - 1; x <= maxX + 1; x++)
            {
                if(slice.TryGetValue((x, y), out var result))
                    Console.Write(result);
                else
                    Console.Write('.');
            }
            
            Console.Write(Environment.NewLine);
        }
    }
}
