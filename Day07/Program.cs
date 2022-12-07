Console.WriteLine("Mickur's Advent of Code 2022 - Day 7!");

// Setup
var input = File.ReadAllLines("input.txt");

var rootDir = new CustomDirectory("root");
var currentDir = rootDir;

foreach (var line in input)
{
    // Ignore these lines
    if (line == "$ ls")
        continue;

    if (line.StartsWith("dir "))
        continue;

    // Changing directory
    if (line.StartsWith("$ cd "))
    {
        var output = line.Split(' ');
        
        switch (output[2])
        {
            case "/":
                currentDir = rootDir;
                continue;
            
            case "..":
                currentDir = currentDir.parent;
                continue;
            
            default:
                // Check if it already exists
                if (currentDir.children.Exists(x => x.name == output[2]))
                    currentDir = currentDir.children.First(x => x.name == output[2]);
                else
                {
                    var newDir = new CustomDirectory($"{output[2]}", currentDir);
                    currentDir.children.Add(newDir);
                    currentDir = newDir;
                }
                
                continue;
        }
    }
    
    // Output
    else
    {
        var output = line.Split(' ');
        
        if (long.TryParse(output[0], out var result))
        {
            currentDir.files.Add(output[1], result);;
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
    public string name;
    public Dictionary<string, long> files = new ();
    public CustomDirectory parent;
    public List<CustomDirectory> children = new();

    public CustomDirectory(string name, CustomDirectory parent = null)
    {
        this.name = name;
        this.parent = parent;
    }

    public long GetDirectorySize()
    {
        long value = 0;

        foreach (var subDir in children)
        {
            value += subDir.GetDirectorySize();
        }

        foreach (var file in files)
        {
            value += file.Value;
        }

        return value;
    }

    public long GetSumOfDirectoriesBelowSize(long max)
    {
        long value = 0;
        long dirSize = 0;
        
        // Keep value from subdirs
        foreach (var subDir in children)
        {
            value += subDir.GetSumOfDirectoriesBelowSize(max);
        }

        // Add to value if we have a total below max
        foreach (var subDir in children)
        {
            dirSize += subDir.GetDirectorySize();
        }
        foreach (var file in files)
        {
            dirSize += file.Value;
        }

        if (dirSize <= max)
            value += dirSize;
        
        return value;
    }

    public List<long> GetListOfDirectoriesAboveSize(long minSize)
    {
        long dirSize = 0;
        var sizes = new List<long>();
        
        // Keep sizes from subdirs
        foreach (var subDir in children)
        {
            sizes.AddRange(subDir.GetListOfDirectoriesAboveSize(minSize));
        }

        // Add to sizes if we have a total above min
        foreach (var subDir in children)
        {
            dirSize += subDir.GetDirectorySize();
        }
        foreach (var file in files)
        {
            dirSize += file.Value;
        }

        if (dirSize >= minSize)
            sizes.Add(dirSize);
        
        return sizes;
    }
}
