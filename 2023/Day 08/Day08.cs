using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day08 : Day
{
    public static void Main()
    {
        var instance = new Day08();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    private const string Start = "AAA";
    private const string Destination = "ZZZ";

    static Instruction[] ReadInstruction(string[] input)
    {
        var instruction = input.First();
        return instruction.Select(c => c == 'L' ? Instruction.Left : Instruction.Right).ToArray();
    }

    static Dictionary<string, (string Left, string Right, bool Start, bool End, int Steps)> ReadNetwork(string[] input)
    {
        var network = new Dictionary<string, (string Left, string Right, bool Start, bool End, int Steps)>();
        var lines = input.Skip(2);
        foreach (var line in lines)
        {
            var node = line[..3];
            var left = line.Substring(7, 3);
            var right = line.Substring(12, 3);
            var lastChar = node.Last();
            var start = lastChar == 'A';
            var end = lastChar == 'Z';

            network.Add(node, (left, right, start, end, 0));
        }

        return network;
    }

    public override long GetTask1Result(string[] input)
    {
        var instructions = ReadInstruction(input);
        var network = ReadNetwork(input);

        var steps = 0;

        var node = Start;

        while (node != Destination)
        {
            var way = instructions[steps % instructions.Length];
            node = way == Instruction.Left ? network[node].Left : network[node].Right;
            steps++;
        }

        return steps;
    }

    public override long GetTask2Result(string[] input)
    {
        var instructions = ReadInstruction(input);
        var network = ReadNetwork(input);
        var startNodes = network.Where(pair => pair.Value.Start).Select(pair => pair.Key).ToHashSet();
        var nodesSteps = new Dictionary<string, int>();
        foreach (var startNode in startNodes)
        {
            var steps = 0;

            var node = startNode;

            while (!network[node].End)
            {
                var way = instructions[steps % instructions.Length];
                node = way == Instruction.Left ? network[node].Left : network[node].Right;
                steps++;
            }

            nodesSteps.Add(startNode, steps);
        }

        var result = LCM(nodesSteps.Select(pair => (long)pair.Value).ToArray());

        return result;
    }

    static long LCM(long[] numbers)
    {
        return numbers.Aggregate(LCM);
    }
    static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }
}
