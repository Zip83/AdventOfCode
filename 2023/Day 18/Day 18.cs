using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day18 : Day
{
    private const char GroundLevelTerrain = '.';
    private const char Trench = '#';

    public static void Main()
    {
        var instance = new Day18();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private static Instruction[] ParseInput(string[] input)
    {
        return input.Select(line => new Instruction(line)).ToArray();
    }

    private (int MinRows, int MaxRows, int MinCols, int MaxCols) FindLagoonSize(IEnumerable<Instruction> instructions)
    {
        var rows = 0;
        var cols = 0;
        var maxRows = 0;
        var maxCols = 0;
        var minRows = 0;
        var minCols = 0;

        foreach (var instruction in instructions)
        {
            switch (instruction.Direction)
            {
                case Direction.R:
                    cols += instruction.Length;
                    break;
                case Direction.L:
                    cols -= instruction.Length;
                    break;
                case Direction.D:
                    rows += instruction.Length;
                    break;
                case Direction.U:
                    rows -= instruction.Length;
                    break;
            }

            if (cols < minCols)
            {
                minCols = cols;
            }
            if (rows < minRows)
            {
                minRows = rows;
            }
            if (cols > maxCols)
            {
                maxCols = cols;
            }
            if (rows > maxRows)
            {
                maxRows = rows;
            }
        }

        return (minRows, maxRows, minCols, maxCols);
    }

    private static Dictionary<int, Dictionary<int, char>> InitLagoon(int minRows, int maxRows, int minCols, int maxCols)
    {
        var lagoon = new Dictionary<int, Dictionary<int, char>>();
        for (var row = minRows; row <= maxRows; row++)
        {
            lagoon[row] = new Dictionary<int, char>();
            for (var col = minCols; col <= maxCols; col++)
            {
                lagoon[row][col] = GroundLevelTerrain;
            }
        }

        return lagoon;
    }

    private void CreateLagoonEdge(IEnumerable<Instruction> instructions, Dictionary<int, Dictionary<int, char>> lagoon)
    {
        var row = 0;
        var col = 0;
        foreach (var instruction in instructions)
        {
            switch (instruction.Direction)
            {
                case Direction.R:
                    for (var i = 1; i <= instruction.Length; i++)
                    {
                        lagoon[row][col + i] = Trench;
                    }

                    col += instruction.Length;
                    break;
                case Direction.L:
                    for (var i = 1; i <= instruction.Length; i++)
                    {
                        lagoon[row][col - i] = Trench;
                    }

                    col -= instruction.Length;
                    break;
                case Direction.D:
                    for (var i = 1; i <= instruction.Length; i++)
                    {
                        lagoon[row +i][col] = Trench;
                    }

                    row += instruction.Length;
                    break;
                case Direction.U:
                    for (var i = 1; i <= instruction.Length; i++)
                    {
                        lagoon[row - i][col] = Trench;
                    }

                    row -= instruction.Length;
                    break;
            }
        }
    }

    private static void PrintLagoon(Dictionary<int, Dictionary<int, char>> lagoon)
    {
        foreach (var t in lagoon)
        {
            Console.WriteLine(string.Concat(t.Value.Select(pair => pair.Value)));
        }
    }

    // 1. Set Q to the empty queue or stack.
    // 2. Add node to the end of Q.
    // 3. While Q is not empty:
    // 4.   Set n equal to the first element of Q.
    // 5.   Remove first element from Q.
    // 6.   If n is Inside:
    // Set the n
    //     Add the node to the west of n to the end of Q.
    //     Add the node to the east of n to the end of Q.
    //     Add the node to the north of n to the end of Q.
    //     Add the node to the south of n to the end of Q.
    // 7. Continue looping until Q is exhausted.
    // 8. Return.
    private void SeedFill(Dictionary<int, Dictionary<int, char>> lagoon)
    {
        var stack = new Stack<(int Row, int Col)>();
        stack.Push((1, 1));
        while (stack.Count > 0)
        {
            var node = stack.Pop();
            try
            {
                if (lagoon[node.Row][node.Col] == GroundLevelTerrain)
                {
                    lagoon[node.Row][node.Col] = Trench;
                    stack.Push((node.Row, node.Col - 1)); // to west
                    stack.Push((node.Row, node.Col + 1)); // to east
                    stack.Push((node.Row - 1, node.Col)); // to north
                    stack.Push((node.Row + 1, node.Col)); // to south
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        PrintLagoon(lagoon);
    }

    public override long GetTask1Result(string[] input)
    {
        var instructions = ParseInput(input).ToArray();
        var (minRows, maxRows, minCols, maxCols) = FindLagoonSize(instructions);
        var lagoon = InitLagoon(minRows, maxRows, minCols, maxCols);

        CreateLagoonEdge(instructions, lagoon);
        PrintLagoon(lagoon);
        Console.WriteLine($"Lagoon edge size: {lagoon.Sum(row => row.Value.Sum(col => col.Value == Trench ? 1 : 0))}");

        SeedFill(lagoon);
        PrintLagoon(lagoon);
        var lagoonSize = lagoon.Sum(row => row.Value.Sum(col => col.Value == Trench ? 1 : 0));
        Console.WriteLine($"Lagoon total size: {lagoonSize}");
        return lagoonSize;
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
