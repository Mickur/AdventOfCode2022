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
                // Check if it already exists
                if (currentDir.Children.Exists(x => x.Name == output[2]))
                    currentDir = currentDir.Children.First(x => x.Name == output[2]);
                else
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
        if (int.TryParse(output[0], out var result))
        {
            currentDir.Files.Add(output[1], result);;
        }
    }
}

var answerA = rootDir.GetSumOfDirectoriesBelowSize(100000);

var rootSize = rootDir.GetDirectorySize();
var needToFree = -70000000 + 30000000 + rootSize;

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

        foreach (var subDir in Children)
        {
            value += subDir.GetDirectorySize();
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
        foreach (var subDir in Children)
        {
            value += subDir.GetSumOfDirectoriesBelowSize(max);
        }

        // Add to value if we have a total below max
        foreach (var subDir in Children)
        {
            dirSize += subDir.GetDirectorySize();
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
        foreach (var subDir in Children)
        {
            sizes.AddRange(subDir.GetListOfDirectoriesAboveSize(minSize));
        }

        // Add to sizes if we have a total above min
        foreach (var subDir in Children)
        {
            dirSize += subDir.GetDirectorySize();
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
