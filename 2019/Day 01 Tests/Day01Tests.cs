using NUnit.Framework;

namespace AdventOfCode._2019.Tests;

public class Day01Tests
{
    [TestCase("12", ExpectedResult = 2)]
    [TestCase("14", ExpectedResult = 2)]
    [TestCase("1969", ExpectedResult = 654)]
    [TestCase("100756", ExpectedResult = 33583)]
    public long GetTask1ResultTest(string line)
    {
        var input = new[] { line };
        return new Day01().GetTask1Result(input);
    }
}
