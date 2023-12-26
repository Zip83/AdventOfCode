using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day15Tests
{
    [TestCase("rn=1", ExpectedResult = 30L)]
    [TestCase("cm-", ExpectedResult = 253L)]
    [TestCase("qp=3", ExpectedResult = 97L)]
    [TestCase("cm=2", ExpectedResult = 47L)]
    [TestCase("qp-", ExpectedResult = 14L)]
    [TestCase("pc=4", ExpectedResult = 180L)]
    [TestCase("ot=9", ExpectedResult = 9L)]
    [TestCase("ab=5", ExpectedResult = 197L)]
    [TestCase("pc-", ExpectedResult = 48L)]
    [TestCase("pc=6", ExpectedResult = 214L)]
    [TestCase("ot=7", ExpectedResult = 231L)]
    public long HashTest(string input)
    {
        return Day15.Hash(input);
    }
    
    [TestCase("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", ExpectedResult = 1320L)]
    public long GetTask1ResultTest(string input)
    {
        var day = new Day15();
        return day.GetTask1Result(new [] { input });
    }
    
    [TestCaseSource(nameof(_getTask2ResultTestData))]
    public void GetTask2ResultTest(string[] input, long expectedResult)
    {
        var day = new Day23();
        var result = day.GetTask2Result(input);
        result.Should().Be(expectedResult);
    }
    
    private static object[] _getTask2ResultTestData =
    {
        new object[]
        {
            new[]
            {
                "#.#####################",
                "#.......#########...###",
                "#######.#########.#.###",
                "###.....#.>.>.###.#.###",
                "###v#####.#v#.###.#.###",
                "###.>...#.#.#.....#...#",
                "###v###.#.#.#########.#",
                "###...#.#.#.......#...#",
                "#####.#.#.#######.#.###",
                "#.....#.#.#.......#...#",
                "#.#####.#.#.#########v#",
                "#.#...#...#...###...>.#",
                "#.#.#v#######v###.###v#",
                "#...#.>.#...>.>.#.###.#",
                "#####v#.#.###v#.#.###.#",
                "#.....#...#...#.#.#...#",
                "#.#########.###.#.#.###",
                "#...###...#...#...#.###",
                "###.###.#.###v#####v###",
                "#...#...#.#.>.>.#.>.###",
                "#.###.###.#.###.#.#v###",
                "#.....###...###...#...#",
                "#####################.#"
            },
            154
        }
    };
}