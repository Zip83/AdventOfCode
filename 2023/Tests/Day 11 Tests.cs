using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day11Tests
{
    [TestCaseSource(nameof(ExpandUniverseTestData))]
    public void ExpandUniverseTest(string[] input, string[] expectedExpandedUniverse)
    {
        var result = Day11.ExpandUniverse(input);
        var expectedResults = new char[expectedExpandedUniverse.Length][];
        for (var i = 0; i < expectedExpandedUniverse.Length; i++)
        {
            expectedResults[i] = expectedExpandedUniverse[i].ToCharArray();
        }

        result.Should().BeEquivalentTo(expectedResults);
    }

    [TestCaseSource(nameof(CreateGalaxyMapTestData))]
    public void CreateGalaxyMap(string[] expandedUniverse, int[][] expectedResult)
    {
        var universe = new char[expandedUniverse.Length][];
        for (var i = 0; i < expandedUniverse.Length; i++)
        {
            universe[i] = expandedUniverse[i].ToCharArray();
        }
        var result = Day11.CreateGalaxyMap(universe);
        result.Should().BeEquivalentTo(expectedResult);
    }

    [TestCaseSource(nameof(GetTotalPathLengthTestData))]
    public void GetTotalPathLengthTest(string[] input, int expectedResult)
    {
        var result = Day11.GetTotalPathLength(input);
        result.Should().Be(expectedResult);
    }

    public static object[][] ExpandUniverseTestData =
    {
        new object[]
        {
            new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            },
            new[]
            {
                "....#........",
                ".........#...",
                "#............",
                ".............",
                ".............",
                "........#....",
                ".#...........",
                "............#",
                ".............",
                ".............",
                ".........#...",
                "#....#.......",
            }
        }
    };

    public static object[][] CreateGalaxyMapTestData =
    {
        new object[]
        {
            new[]
            {
                "....#........",
                ".........#...",
                "#............",
                ".............",
                ".............",
                "........#....",
                ".#...........",
                "............#",
                ".............",
                ".............",
                ".........#...",
                "#....#.......",
            },
            new []
            {
                new [] {0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0},
                new [] {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0},
                new [] {0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0},
                new [] {8, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0}
            }
        }
    };

    public static object[] GetTotalPathLengthTestData =
    {
        new object[]
        {
            new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            },
            374
        }
    };

    [TestCaseSource(nameof(Task2GetTotalPathLengthTestData))]
    public void GetTotalPathLengthTest(string[] input, int expandCoefficient, long expectedResult)
    {
        var result = Day11.GetTotalPathLength(input, expandCoefficient);
        result.Should().Be(expectedResult);
    }

    public static object[][] Task2GetTotalPathLengthTestData =
    {
        new object[]
        {
            new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            },
            2,
            374L
        },
        new object[] {
            new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            },
            10,
            1030L
        },
        new object[]
        {
            new[]
            {
                "...#......",
                ".......#..",
                "#.........",
                "..........",
                "......#...",
                ".#........",
                ".........#",
                "..........",
                ".......#..",
                "#...#....."
            },
            100,
            8410L
        }
    };
}
