using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day13Tests
{
    [TestCaseSource(nameof(_getTask1ResultTestData))]
    public void GetTask1ResultTest(string[] input, long expectedResult)
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
    
    [TestCaseSource(nameof(_getTask2ResultTestData))]
    public void GetTask2ResultTest(string[] input, long expectedResult)
    {
        var day = new Day13();
        var result = day.GetTask2Result(input);
        result.Should().Be(expectedResult);
    }

    private static object[] _getTask2ResultTestData =
    {
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
                "#.#.##.#.",
            },
            300
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
            },
            100
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
                "#.#.##.#.",
                "",
                "#...##..#",
                "#....#..#",
                "..##..###",
                "#####.##.",
                "#####.##.",
                "..##..###",
                "#....#..#",
            },
            400
        },
    };
}