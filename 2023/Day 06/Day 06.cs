using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day06 : Day
{
    public static void Main()
    {
        var instance = new Day06();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] lines)
    {
        // Parse input data
        var raceTimes = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1) // Skip the "Time:" label
            .Select(int.Parse)
            .ToArray();

        var recordDistances = lines[1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1) // Skip the "Distance:" label
            .Select(int.Parse)
            .ToArray();

        // Calculate the number of ways to beat the record in each race
        var totalWays = 1;
        for (var i = 0; i < raceTimes.Length; i++)
        {
            var raceTime = raceTimes[i];
            var recordDistance = recordDistances[i];

            // Calculate the number of ways to beat the record for the current race
            var waysToBeatRecord = 0;
            for (var holdTime = 0; holdTime < raceTime; holdTime++)
            {
                var speed = holdTime;
                var remainingTime = raceTime - holdTime;
                var distance = speed * remainingTime;

                if (distance > recordDistance)
                {
                    waysToBeatRecord++;
                }
            }

            // Update the total number of ways
            totalWays *= waysToBeatRecord;
        }

        // Output the result
        Console.WriteLine("Total ways to beat the record: " + totalWays);

        return totalWays;
    }

    public override long GetTask2Result(string[] lines)
    {
        // Parse input data
        var raceTime = long.Parse(string.Join("",
            lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)));

        var recordDistance = long.Parse(string.Join("",
            lines[1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)));

        // Calculate the number of ways to beat the record in each race
        var totalWays = 1L;

        // Calculate the number of ways to beat the record for the current race
        var waysToBeatRecord = 0L;
        for (var holdTime = 0L; holdTime < raceTime; holdTime++)
        {
            var speed = holdTime;
            var remainingTime = raceTime - holdTime;
            var distance = speed * remainingTime;

            if (distance > recordDistance)
            {
                waysToBeatRecord++;
            }
        }

        // Update the total number of ways
        totalWays *= waysToBeatRecord;

        // Output the result
        Console.WriteLine("Total ways to beat the record: " + totalWays);

        return totalWays;
    }
}
