using AoCUtils;

Console.WriteLine("Mickur's Advent of Code 2022 - Day 7!");

// Setup
var input = File.ReadAllLines("input.txt");

var rootDir = new CustomDirectory("root");
var currentDir = rootDir;

foreach (var line in input)
{
    // Ignore these lines
    if (line.StartsWith("dir ") || line == "$ ls")
        continue;
    
    var output = line.Split(' ');

    // Changing directory
    if (line.StartsWith("$ cd "))
    {
        switch (output[2])
        {
            case "/":
                currentDir = rootDir;
                break;
            
            case "..":
                currentDir = currentDir.Parent;
                break;
            
            default:
                var newDirSet = false;
                
                for (var i = 0; i < currentDir.Children.Count; i++)
                {
                    if (currentDir.Children[i].Name == output[2])
                    {
                        currentDir = currentDir.Children[i];
                        newDirSet = true;
                        break;
                    }
                }

                if (!newDirSet)
                {
                    var newDir = new CustomDirectory($"{output[2]}", currentDir);
                    currentDir.Children.Add(newDir);
                    currentDir = newDir;
                }
                break;
        }
    }
    
    // Output
    else
    {
        currentDir.Files.Add(output[1], Parsing.FastIntParse(output[0]));
    }
}

var rootSize = rootDir.GetDirectorySize();
var needToFree = -70000000 + 30000000 + rootSize;

var answerA = rootDir.GetSumOfDirectoriesBelowSize(100000);
var answerB = rootDir.GetListOfDirectoriesAboveSize(needToFree).Min();

Console.WriteLine(answerA);
Console.WriteLine(answerB);

class CustomDirectory
{
    public readonly string Name;
    public readonly Dictionary<string, int> Files = new ();
    public readonly CustomDirectory Parent;
    public readonly List<CustomDirectory> Children = new();

    public CustomDirectory(string name, CustomDirectory parent = null)
    {
        Name = name;
        Parent = parent;
    }

    public int GetDirectorySize()
    {
        var value = 0;

        for (var i = 0; i < Children.Count; i++)
        {
            value += Children[i].GetDirectorySize();
        }

        foreach (var file in Files)
        {
            value += file.Value;
        }

        return value;
    }

    public int GetSumOfDirectoriesBelowSize(int max)
    {
        var value = 0;
        var dirSize = 0;
        
        // Keep value from subdirs
        for (var i = 0; i < Children.Count; i++)
        {
            value += Children[i].GetSumOfDirectoriesBelowSize(max);
        }

        // Add to value if we have a total below max
        for (var i = 0; i < Children.Count; i++)
        {
            dirSize += Children[i].GetDirectorySize();
        }
        
        foreach (var file in Files)
        {
            dirSize += file.Value;
        }

        if (dirSize <= max)
            value += dirSize;
        
        return value;
    }

    public List<int> GetListOfDirectoriesAboveSize(int minSize)
    {
        var dirSize = 0;
        var sizes = new List<int>();
        
        // Keep sizes from subdirs
        for (var i = 0; i < Children.Count; i++)
        {
            sizes.AddRange(Children[i].GetListOfDirectoriesAboveSize(minSize));
        }

        // Add to sizes if we have a total above min
        for (var i = 0; i < Children.Count; i++)
        {
            dirSize += Children[i].GetDirectorySize();
        }
        
        foreach (var file in Files)
        {
            dirSize += file.Value;
        }

        if (dirSize >= minSize)
            sizes.Add(dirSize);
        
        return sizes;
    }
}
