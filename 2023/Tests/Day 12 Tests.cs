using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2023.Tests;

public class Day12Tests
{
    [TestCaseSource(nameof(GetTask1ResultTestData))]
    public void GetTask1ResultTests(string[] input, int expectedResult)
    {
        var day = new Day12();
        var result = day.GetTask1Result(input);
        result.Should().Be(expectedResult);
    }

    public static object[][] GetTask1ResultTestData =
    {
        new object[] {
            new[]
            {
                "???.### 1,1,3"
            },
            1
        },
        new object[] {
            new[]
            {
                ".??..??...?##. 1,1,3"
            },
            4
        },
        new object[] {
            new[]
            {
                "?###???????? 3,2,1"
            },
            10
        }
    };
}
