using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day07Tests
{
    [TestCase("AAAAA", false, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("AA8AA", false, ExpectedResult = HandStrength.FourOfAKind)]
    [TestCase("23332", false, ExpectedResult = HandStrength.FullHouse)]
    [TestCase("TTT98", false, ExpectedResult = HandStrength.ThreeOfAKind)]
    [TestCase("23432", false, ExpectedResult = HandStrength.TwoPair)]
    [TestCase("A23A4", false, ExpectedResult = HandStrength.OnePair)]
    [TestCase("23456", false, ExpectedResult = HandStrength.HighCard)]
    [TestCase("AAAAA", true, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("AA8AA", true, ExpectedResult = HandStrength.FourOfAKind)]
    [TestCase("AAJAA", true, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("JJ8JJ", true, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("23332", true, ExpectedResult = HandStrength.FullHouse)]
    [TestCase("2JJJ2", true, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("J333J", true, ExpectedResult = HandStrength.FiveOfKind)]
    [TestCase("TTT98", true, ExpectedResult = HandStrength.ThreeOfAKind)]
    [TestCase("TTTJ8", true, ExpectedResult = HandStrength.FourOfAKind)]
    [TestCase("JJJ98", true, ExpectedResult = HandStrength.FourOfAKind)]
    [TestCase("23432", true, ExpectedResult = HandStrength.TwoPair)]
    [TestCase("A23A4", true, ExpectedResult = HandStrength.OnePair)]
    [TestCase("J3773", true, ExpectedResult = HandStrength.FullHouse)]
    [TestCase("J23J4", true, ExpectedResult = HandStrength.ThreeOfAKind)]
    [TestCase("A2JA4", true, ExpectedResult = HandStrength.ThreeOfAKind)]
    [TestCase("23456", true, ExpectedResult = HandStrength.HighCard)]
    [TestCase("2345J", true, ExpectedResult = HandStrength.OnePair)]
    [TestCase("2J4J6", true, ExpectedResult = HandStrength.ThreeOfAKind)]
    [TestCase("2JJJ6", true, ExpectedResult = HandStrength.FourOfAKind)]
    [TestCase("2JJJJ", true, ExpectedResult = HandStrength.FiveOfKind)]
    public HandStrength GetHandTypeTest(string hand, bool allowJokers)
    {
        return Day07.GetHandType(hand, allowJokers);
    }
}
