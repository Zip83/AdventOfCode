// using NUnit.Framework;
// using NUnit.Framework.Legacy;
//
// namespace Day_05;
//
// static class Task2
// {
//     const string FileName = "input.csv";
//
//     public static long GetResult()
//     {
//         // Read input from file
//         var seedRanges = ReadSeedRangesFromFile(FileName);
//         var maps = ReadMapsFromFile(FileName);
//
//         var locations = seedRanges.Select(tuple =>
//                 SeedRangeToLocation("seed", tuple.Item1, tuple.Item2, maps).Min(tuple => tuple.Item1))
//             .ToList();
//
//         Console.WriteLine(string.Join(", ", locations.Select((l, i) => $"({l}, {i})")));
//
//         var lowestLocation = locations.Min();
//
//         // Output the result
//         Console.WriteLine($"The lowest location number is: {lowestLocation}");
//
//         return lowestLocation;
//     }
//
//     static List<(long, long)> ReadSeedRangesFromFile(string fileName)
//     {
//         var lines = File.ReadAllLines(fileName);
//         var seedRanges = lines[0].Split(' ').Skip(1).Select(long.Parse).ToList();
//         var result = new List<(long, long)>();
//
//         for (int i = 0; i < seedRanges.Count; i += 2)
//         {
//             result.Add((seedRanges[i], seedRanges[i] + seedRanges[i + 1]));
//         }
//
//         return result;
//     }
//
//     static Dictionary<string, List<(long, long, long)>> ReadMapsFromFile(string fileName)
//     {
//         var lines = File.ReadAllLines(fileName);
//         var maps = new Dictionary<string, List<(long, long, long)>>();
//         string currentMapType = null;
//         List<(long, long, long)> currentMap = null;
//
//         for (var i = 2; i < lines.Length; i++)
//         {
//             var line = lines[i].Trim();
//
//             if (line.EndsWith("map:"))
//             {
//                 // Start of a new map type
//                 currentMapType = line.Substring(0, line.Length - 5);
//                 currentMap = new List<(long, long, long)>();
//             }
//             else if (!string.IsNullOrEmpty(line))
//             {
//                 // Process mapping lines
//                 var parts = line.Split(' ');
//                 currentMap.Add((long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
//             }
//             else
//             {
//                 // Empty line indicates the end of the current map type
//                 if (currentMapType != null)
//                 {
//                     maps[currentMapType] = currentMap;
//                     currentMapType = null;
//                     currentMap = null;
//                 }
//             }
//         }
//
//         if (currentMapType != null)
//         {
//             maps[currentMapType] = currentMap;
//         }
//
//         return maps;
//     }
//
//     static List<(long, long)> SeedRangeToLocation(string source, long seedStart, long seedEnd,
//         Dictionary<string, List<(long, long, long)>> maps)
//     {
//         var ranges = new Stack<(long, long)>();
//         ranges.Push((seedStart, seedEnd));
//         var newRanges = new List<(long, long)>();
//
//         foreach (var (a, b, c) in maps)
//         {
//             while (ranges.Count > 0)
//             {
//                 var (s, e) = ranges.Pop();
//                 foreach (var (a, b, c) in maps[mapType])
//                 {
//                     var os = Math.Max(s, b);
//                     var oe = Math.Min(e, b + c);
//                     if (os < oe)
//                     {
//                         newRanges.Add((os - b + a, oe - b + a));
//                         if (os > s)
//                         {
//                             ranges.Push((s, os));
//                         }
//
//                         if (e > oe)
//                         {
//                             ranges.Push((oe, e));
//                         }
//
//                         break;
//                     }
//                 }
//             }
//
//             if (!newRanges.Any())
//             {
//                 newRanges.Add((seedStart, seedEnd));
//             }
//
//             var newSource = mapType.Split('-').Last();
//             newRanges = newRanges.SelectMany(tuple => SeedRangeToLocation(newSource, tuple.Item1, tuple.Item2, maps))
//                 .ToList();
//         }
//
//         return newRanges;
//     }
// }