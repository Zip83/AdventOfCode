using NUnit.Framework;

namespace AdventOfCode._2015.Tests;

public class Day01Tests
{
    [TestCase("(())", ExpectedResult = 0)]
    [TestCase("()()", ExpectedResult = 0)]
    [TestCase("(((", ExpectedResult = 3)]
    [TestCase("(()(()(", ExpectedResult = 3)]
    [TestCase("))(((((", ExpectedResult = 3)]
    [TestCase("())", ExpectedResult = -1)]
    [TestCase("))(", ExpectedResult = -1)]
    [TestCase(")))", ExpectedResult = -3)]
    [TestCase(")())())", ExpectedResult = -3)]
    public long GetTask1ResultTest(string input)
    {
        var day = new Day01();
        return day.GetTask1Result(new [] {input});
    }
    
    [TestCase(")", ExpectedResult = 1)]
    [TestCase("()())", ExpectedResult = 5)]
    public long GetTask2ResultTest(string input)
    {
        var day = new Day01();
        return day.GetTask2Result(new [] {input});
    }
}