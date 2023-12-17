using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day09Tests
{
    [TestCaseSource(nameof(_task1TestData))]
    public void GetTask1ResultTest(string[] input, int expectedResult)
    {
        var day = new Day09();
        var result = day.GetTask1Result(input);
        result.Should().Be(expectedResult);
    }

    static object[][] _task1TestData =
    {
        new object[]
        {
            new []
            {
                "0 3 6 9 12 15"
            },
            18
        },
        new object[]
        {
            new []
            {
                "1 3 6 10 15 21"
            },
            28
        },
        new object[]
        {
            new []
            {
                "10 13 16 21 30 45"
            },
            68
        }
    };

    [TestCaseSource(nameof(_task2TestData))]
    public void GetTask2ResultTest(string[] input, int expectedResult)
    {
        var day = new Day09();
        var result = day.GetTask2Result(input);
        result.Should().Be(expectedResult);
    }

    static object[][] _task2TestData =
    {
        new object[]
        {
            new []
            {
                "10 13 16 21 30 45"
            },
            5
        },
        new object[]
        {
            new []
            {
                "0 3 6 9 12 15"
            },
            -3
        },
        new object[]
        {
            new []
            {
                "1 3 6 10 15 21"
            },
            0
        },
        new object[]
        {
            new []
            {
                "10 13 16 21 30 45"
            },
            5
        }
    };
}
