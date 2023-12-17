using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day07 : Day
{
    private Dictionary<char, int> _cardStrength = new Dictionary<char, int>();

    public static void Main()
    {
        var instance = new Day07();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private static readonly string[] Task1CardLabels =
        "A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, 2".Split(",").Select(s => s.Trim()).ToArray();

    public override long GetTask1Result(string[] input)
    {
        _cardStrength = Task1CardLabels.Select((value, index) => new { value, index })
            .ToDictionary(pair => char.Parse(pair.value), pair => Task1CardLabels.Length - pair.index);

        var hands = ReadHands(input);
        hands.Sort((hand1, hand2) => HandComparison(hand1, hand2, false));

        // hands.ForEach(tuple => Console.WriteLine($"Hand ({tuple.Item1}) has strength {GetHandType(tuple.Item1)}"));

        var totalWinnings = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Item2 * (i + 1);
        }

        return totalWinnings;
    }

    static List<(string, int)> ReadHands(string[] lines)
    {
        var hands = new List<(string, int)>();
        foreach (var line in lines)
        {
            var l = line.Split(" ");
            hands.Add((l[0], int.Parse(l[1])));
        }

        return hands;
    }

    private int HandComparison((string, int) hand1, (string, int) hand2, bool allowJokers)
    {
        var hand1Type = GetHandType(hand1.Item1, allowJokers);
        var hand2Type = GetHandType(hand2.Item1, allowJokers);

        if (hand1Type != hand2Type)
        {
            return hand1Type - hand2Type;
        }

        for (var i = 0; i < hand1.Item1.Length; i++)
        {
            var comparision = _cardStrength[hand1.Item1[i]] - _cardStrength[hand2.Item1[i]];
            if (comparision != 0)
            {
                return comparision;
            }
        }

        return 0;
    }

    private static readonly string[] Task2CardLabels =
        "A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2, J".Split(",").Select(s => s.Trim()).ToArray();

    public override long GetTask2Result(string[] input)
    {
        _cardStrength = Task2CardLabels.Select((value, index) => new { value, index })
            .ToDictionary(pair => char.Parse(pair.value), pair => Task2CardLabels.Length - pair.index);

        var hands = ReadHands(input);
        hands.Sort((hand1, hand2) => HandComparison(hand1, hand2, true));

        hands.ForEach(tuple =>
        {
            // Console.WriteLine($"Hand ({tuple.Item1}) has strength {GetHandType(tuple.Item1)}");
            File.AppendAllLines("task2.csv", new[] { tuple.Item1 });
        });

        var totalWinnings = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Item2 * (i + 1);
        }

        return totalWinnings;
    }

    public static HandStrength GetHandType(string hand, bool allowJokers)
    {
        var cards = hand.ToCharArray();
        var uniqueNumberOfCards = cards.Distinct().Count();
        var jokers = allowJokers ? hand.Sum(c => c == 'J' ? 1 : 0) : 0;
        switch (uniqueNumberOfCards)
        {
            case 1:
                return HandStrength.FiveOfKind;
            case 2:
            {
                var cardLabels = new Dictionary<char, int>();
                foreach (var card in cards)
                {
                    try
                    {
                        cardLabels[card]++;
                    }
                    catch
                    {
                        cardLabels[card] = 1;
                    }
                }

                return cardLabels.Max(pair => pair.Value) == 4
                    ? jokers is 1 or 4 ? HandStrength.FiveOfKind : HandStrength.FourOfAKind
                    : jokers > 1
                        ? HandStrength.FiveOfKind
                        : HandStrength.FullHouse;
            }
            case 3:
            {
                var cardLabels = new Dictionary<char, int>();
                foreach (var card in cards)
                {
                    try
                    {
                        cardLabels[card]++;
                    }
                    catch
                    {
                        cardLabels[card] = 1;
                    }
                }

                if (cardLabels.Max(pair => pair.Value) == 3)
                {
                    return
                        jokers switch
                        {
                            0 => HandStrength.ThreeOfAKind,
                            _ => HandStrength.FourOfAKind
                        };
                }

                return jokers == 2
                    ? HandStrength.FourOfAKind
                    : jokers == 1
                        ? HandStrength.FullHouse
                        : HandStrength.TwoPair;
            }
            case 4:
                if (jokers == 2)
                {
                    return HandStrength.ThreeOfAKind;
                }

                return jokers == 1 ? HandStrength.ThreeOfAKind : HandStrength.OnePair;
            default:
                return jokers == 1 ? HandStrength.OnePair : HandStrength.HighCard;
        }
    }
}
