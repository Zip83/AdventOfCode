namespace AdventOfCode._2023;

public struct Instruction
{
    public Direction Direction { get; }
    public long Length { get; }

    public Instruction(Direction direction, long length)
    {
        Direction = direction;
        Length = length;
    }
}
