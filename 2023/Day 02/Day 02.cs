using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day02 : Day
{
    const string GameIdPattern = "Game ([0-9]*)";
    const string RedPattern = "([0-9]*) red";
    const string GreenPattern = "([0-9]*) green";
    const string BluePattern = "([0-9]*) blue";

    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    public static void Main()
    {
        var instance = new Day02();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var sum = 0;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }

            // Process line
            //Console.WriteLine(line);
            var gameIdMatch = Regex.Match(line, GameIdPattern);
            //Console.WriteLine(gameIdMatch.Groups[1].Value);
            var sets = line.Split(";");
            var correctSet = true;
            foreach (var set in sets)
            {
                var redMatch = Regex.Match(set, RedPattern);
                if (!string.IsNullOrEmpty(redMatch.Groups[1].Value) && int.Parse(redMatch.Groups[1].Value) > MaxRed)
                {
                    correctSet = false;
                    break;
                }

                var greenMatch = Regex.Match(set, GreenPattern);
                if (!string.IsNullOrEmpty(greenMatch.Groups[1].Value) && int.Parse(greenMatch.Groups[1].Value) > MaxGreen)
                {
                    correctSet = false;
                    break;
                }

                var blueMatch = Regex.Match(set, BluePattern);
                if (!string.IsNullOrEmpty(blueMatch.Groups[1].Value) && int.Parse(blueMatch.Groups[1].Value) > MaxBlue)
                {
                    correctSet = false;
                    break;
                }
            }

            if (correctSet)
            {
                sum += int.Parse(gameIdMatch.Groups[1].Value);
            }
        }

        return sum;
    }

    public override long GetTask2Result(string[] input)
    {
        var sum = 0;

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                continue;
            }

            var sets = line.Split(";");
            var minRed = 1;
            var minGreen = 1;
            var minBlue = 1;
            foreach (var set in sets)
            {
                var redMatch = Regex.Match(set, RedPattern);
                if (redMatch.Success)
                {
                    var red = int.Parse(redMatch.Groups[1].Value);
                    if (red > minRed)
                    {
                        minRed = red;
                    }
                }

                var greenMatch = Regex.Match(set, GreenPattern);
                if (greenMatch.Success)
                {
                    var green = int.Parse(greenMatch.Groups[1].Value);
                    if (green > minGreen)
                    {
                        minGreen = green;
                    }
                }

                var blueMatch = Regex.Match(set, BluePattern);
                if (blueMatch.Success)
                {
                    var blue = int.Parse(blueMatch.Groups[1].Value);
                    if (blue > minBlue)
                    {
                        minBlue = blue;
                    }
                }
            }

            var power = minRed * minGreen * minBlue;
            sum += power;
        }

        return sum;
    }
}
