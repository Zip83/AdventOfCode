namespace AdventOfCode._2023;

public struct Instruction
{
    public Direction Direction { get; }
    public int Length { get; }
    public string Color { get; }

    public Instruction(string input)
    {
        var split = input.Split(" ");

        Direction = Enum.Parse<Direction>(split[0]);
        Length = int.Parse(split[1]);
        Color = split[2].Substring(1, split[2].Length - 2);
    }
}
