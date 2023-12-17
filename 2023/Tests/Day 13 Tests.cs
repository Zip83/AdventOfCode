using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day13Tests
{
    [TestCaseSource(nameof(_getTask1ResultTestData))]
    public void GetTask1Result(string[] input, long expectedResult)
    {
        var day = new Day13();
        var result = day.GetTask1Result(input);
        result.Should().Be(expectedResult);
    }

    private static object[] _getTask1ResultTestData =
    {
        new object[]
        {
            new[]
            {
                "#...##..#",
                "#....#..#",
                "..##..###",
                "#####.##.",
                "#####.##.",
                "..##..###",
                "#....#..#"
            },
            400
        },
        new object[]
        {
            new[]
            {
                "#.##..##.",
                "..#.##.#.",
                "##......#",
                "##......#",
                "..#.##.#.",
                "..##..##.",
                "#.#.##.#."
            },
            5
        },
        new object[]
        {
            new[]
            {
                "#...##..#",
                "#....#..#",
                "..##..###",
                "#####.##.",
                "#####.##.",
                "..##..###",
                "#....#..#",
                "",
                "#.##..##.",
                "..#.##.#.",
                "##......#",
                "##......#",
                "..#.##.#.",
                "..##..##.",
                "#.#.##.#."
            },
            405
        }
        ,new object[]
        {
            new[]
            {
                "##..#..#......#",
                ".........#..#..",
                ".####.#.######.",
                "#....#.###..###",
                "..##..#.#.##.#.",
                "######...#..#..",
                "#.##.#.#.#..#.#",
                "#....#..######.",
                ".#..#...#.##.#.",
                "#....#....##...",
                ".#..#.#..####..",
                "......#.######.",
                "##..##.#.####.#"
            },
            11
        }
    };
}