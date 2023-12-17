using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day05 : Day
{
    public static void Main()
    {
        var instance = new Day05();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        // Read input from file
        var seeds = ReadSeedsFromFile(input);
        var maps = ReadMapsFromFile(input);

        // Perform the mappings
        var locations = seeds.Select(seed => PerformMappings("seed", seed, maps)).ToList();

        // Find the lowest location number
        var lowestLocation = locations.Min();

        // Output the result
        Console.WriteLine($"The lowest location number is: {lowestLocation}");

        return lowestLocation;
    }

    static List<long> ReadSeedsFromFile(string[] input)
    {
        var seeds = input[0].Split(' ').Skip(1).Select(long.Parse).ToList();
        return seeds;
    }

    static Dictionary<string, List<(long, long, long)>> ReadMapsFromFile(string[] input)
    {
        var maps = new Dictionary<string, List<(long, long, long)>>();
        string currentMapType = null;
        List<(long, long, long)> currentMap = null;

        for (var i = 2; i < input.Length; i++)
        {
            var line = input[i].Trim();

            if (line.EndsWith("map:"))
            {
                // Start of a new map type
                currentMapType = line.Substring(0, line.Length - 5);
                currentMap = new List<(long, long, long)>();
            }
            else if (!string.IsNullOrEmpty(line))
            {
                // Process mapping lines
                var parts = line.Split(' ');
                currentMap.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }
            else
            {
                // Empty line indicates the end of the current map type
                if (currentMapType != null)
                {
                    maps[currentMapType] = currentMap;
                    currentMapType = null;
                    currentMap = null;
                }
            }
        }

        if (currentMapType != null)
        {
            maps[currentMapType] = currentMap;
        }

        return maps;
    }

    static long PerformMappings(string source, long seed, Dictionary<string, List<(long, long, long)>> maps)
    {
        var newSeed = seed;

        var mapType = GetNextMapType(source, maps);
        if (!string.IsNullOrEmpty(mapType))
        {
            foreach (var map in maps[mapType])
            {
                if (seed >= map.Item2 && seed < map.Item2 + map.Item3)
                {
                    newSeed = seed - map.Item2 + map.Item1;
                    break;
                }
            }

            var newSource = mapType.Split('-').Last();
            newSeed = PerformMappings(newSource, newSeed, maps);
        }

        return newSeed;
    }

    static string GetNextMapType(string currentMapType, Dictionary<string, List<(long, long, long)>> maps)
    {
        var source = currentMapType.Split('-').Last();
        return maps.FirstOrDefault(pair => pair.Key.StartsWith(source)).Key;
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
