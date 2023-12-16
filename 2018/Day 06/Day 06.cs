using AdventOfCode.Shared;

namespace Day_06;

public class Day17 : Day
{
    private const char SameDistanceChar = '.';

    public static void Main()
    {
        var instance = new Day17();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        var matrix = ReadInput(input);
        var size = InitSize(matrix);

        var candidates = new Dictionary<int, Dictionary<int, char>>();
        for (var i = 0; i < 50; i++)
        {
            foreach (var columns in matrix)
            {
                foreach (var chars in columns.Value)
                {
                    TryGrow(columns.Key - 1, chars.Key, chars.Value, matrix, candidates, size);
                    TryGrow(columns.Key, chars.Key - 1, chars.Value, matrix, candidates, size);
                    TryGrow(columns.Key + 1, chars.Key, chars.Value, matrix, candidates, size);
                    TryGrow(columns.Key, chars.Key + 1, chars.Value, matrix, candidates, size);
                }
            }

            var newCan = new Dictionary<int, Dictionary<int, char>>();
            foreach (var columnsCan in candidates)
            {
                foreach (var charCan in columnsCan.Value)
                {
                    if (charCan.Value != SameDistanceChar)
                    {
                        AddCharToPos(columnsCan.Key, charCan.Key, charCan.Value, matrix);
                        AddCharToPos(columnsCan.Key, charCan.Key, charCan.Value, newCan);
                    }
                }
            }

            candidates = newCan;
        }

        return 0;
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }

    private Dictionary<int, Dictionary<int, char>> ReadInput(string[] input)
    {
        var matrix = new Dictionary<int, Dictionary<int, char>>();

        var ch = 65;
        foreach (var line in input)
        {
            var pos = line.Split(",");
            if (!int.TryParse(pos[0].Trim(), out var x))
            {
                Environment.Exit(1);
            }

            if (!int.TryParse(pos[1].Trim(), out var y))
            {
                Environment.Exit(1);
            }

            Console.WriteLine(x + "x" + y);
            if (!matrix.TryGetValue(x, out var col))
            {
                col = new Dictionary<int, char>();
            }

            col.Add(y, Convert.ToChar(ch++));
            matrix.TryAdd(x, col);
        }

        return matrix;
    }

    private Dictionary<char, int> InitSize(Dictionary<int, Dictionary<int, char>> matrix)
    {
        var size = new Dictionary<char, int>();
        foreach (var columns in matrix)
        {
            foreach (var character in columns.Value)
            {
                size.Add(character.Value, 1);
            }
        }

        return size;
    }

    private bool GetChatAtPos(int x, int y, Dictionary<int, Dictionary<int, char>> matrix, out char ch)
    {
        ch = ' ';
        if (matrix.TryGetValue(x, out var columns))
        {
            if (columns.TryGetValue(y, out var character))
            {
                ch = character;
                return true;
            }
        }

        return false;
    }

    private bool AddCharToPos(int x, int y, char ch, Dictionary<int, Dictionary<int, char>> matrix)
    {
        if (!matrix.ContainsKey(x))
        {
            matrix.Add(x, new Dictionary<int, char>());
        }

        matrix.TryGetValue(x, out var columns);
        if (columns.ContainsKey(y))
        {
            return false;
        }

        columns.Add(y, ch);

        return true;
    }

    private char ReplaceCharAtPos(int x, int y, char ch, Dictionary<int, Dictionary<int, char>> matrix)
    {
        var ret = ' ';
        if (matrix.TryGetValue(x, out var columns))
        {
            columns.TryGetValue(y, out ret);
            columns.Remove(y);
            columns.Add(y, ch);
        }

        return ret;
    }

    private void ChangeSize(Dictionary<char, int> size, char ch, int diff)
    {
        size.TryGetValue(ch, out var s);
        size.Remove(ch);
        size.Add(ch, s + diff);
    }

    private void TryGrow(int x, int y, char ch,
        Dictionary<int, Dictionary<int, char>> matrix,
        Dictionary<int, Dictionary<int, char>> candidates,
        Dictionary<char, int> size)
    {
        if (GetChatAtPos(x, y, matrix, out _))
        {
            return;
        }

        if (AddCharToPos(x, y, ch, candidates))
        {
            ChangeSize(size, ch, 1);
        }
        else
        {
            ChangeSize(size, ch, -1);
            ReplaceCharAtPos(x, y, SameDistanceChar, candidates);
        }
    }
}
