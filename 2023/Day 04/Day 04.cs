using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day04 : Day
{
    public static void Main()
    {
        var instance = new Day04();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var sum = 0;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }

            var cards = line.Split(new char[] { ':', '|' }).Skip(1).Take(2).ToArray();
            var winCards = cards[0].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse);
            var ownCards = cards[1].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse);
            // Console.WriteLine(
            //     $"Winning cards ({string.Join(", ", winCards)}), own cards ({string.Join(", ", ownCards)})");

            var intersect = winCards.Intersect(ownCards);
            // Console.WriteLine($"Match numbers: {string.Join(", ", intersect)}");

            var points = (int)Math.Pow(2, intersect.Count() - 1);
            // Console.WriteLine($"Card {i++} has {intersect.Count()} winning numbers ({string.Join(", ", intersect)}), so it is worth {points} points.");

            sum += points;
        }

        return sum;
    }

    public override long GetTask2Result(string[] games)
    {
        var scratchcards = new Dictionary<int, int>();

        for(var i = 1; i <= games.Length; i++)
        {
            scratchcards.Add(i, 1);
        }

        var card = 1;
        foreach (var game in games)
        {
            var cards = game.Split(':', '|').Skip(1).Take(2).ToArray();
            var winCards = cards[0].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse);
            var ownCards = cards[1].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse);
            // Console.WriteLine(
            // $"Winning cards ({string.Join(", ", winCards)}), own cards ({string.Join(", ", ownCards)})");

            var intersect = winCards.Intersect(ownCards);
            // Console.WriteLine($"Match numbers: {string.Join(", ", intersect)}");

            var matchingNumbers = intersect.Count();
            for (var i = 1; i <= matchingNumbers; i++)
            {
                scratchcards[card + i] += scratchcards[card];
            }

            card++;
        }

        return scratchcards.Sum(pair => pair.Value);
    }
}
