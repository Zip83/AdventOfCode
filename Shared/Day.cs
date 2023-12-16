namespace AdventOfCode.Shared;

public abstract class Day
{
    const string FilePath = "input.txt";

    public long GetTask1Result()
    {
        var input = File.ReadAllLines(FilePath);
        return GetTask1Result(input);
    }

    public abstract long GetTask1Result(string[] input);

    public long GetTask2Result()
    {
        var input = File.ReadAllLines(FilePath);
        return GetTask2Result(input);
    }

    public abstract long GetTask2Result(string[] input);
}
