using AdventOfCode.Shared;

namespace AdventOfCode._2023;

public class Day10 : Day
{
    // O - unknown, need decide of prev. position
    // D - down, U - up, L - left, R - right
    // columns - previous pipe
    // row - current pipe

    //   . - | F 7 L J
    // . x x x x x x x
    // - x O x R L R L
    // | x x O D D U U
    // F x D R x D R O
    // 7 x D L D x O L
    // L x U R R O x U
    // J x U L O L U x
    private readonly Dictionary<char, Dictionary<char, Dir>> _shapeMatrix = CreateShapeMatrix();

    private static Dictionary<char, Dictionary<char, Dir>> CreateShapeMatrix()
    {
        string[] rows =
        {
            " .-|F7LJ",
            ".xxxxxxx",
            "-xOxRLRL",
            "|xxODDUU",
            "FxDRxDRO",
            "7xDLDxOL",
            "LxURROxU",
            "JxULOLUx"
        };

        var matrix = new Dictionary<char, Dictionary<char, Dir>>();

        // Fill the matrix
        for (var i = 1; i < rows.Length; i++)
        {
            var rowKey = rows[i][0];
            matrix[rowKey] = new Dictionary<char, Dir>();

            for (var j = 1; j < rows[i].Length; j++)
            {
                var columnKey = rows[0][j];
                var currentPipe = rows[i][j];

                // Convert the character to the corresponding Dir enum
                Dir direction;
                switch (currentPipe)
                {
                    case 'O':
                        direction = Dir.Decide;
                        break;
                    case 'D':
                        direction = Dir.Down;
                        break;
                    case 'U':
                        direction = Dir.Up;
                        break;
                    case 'L':
                        direction = Dir.Left;
                        break;
                    case 'R':
                        direction = Dir.Right;
                        break;
                    default:
                        direction = Dir.Undefined;
                        break;
                }

                matrix[rowKey][columnKey] = direction;
            }
        }

        return matrix;
    }

    public static void Main()
    {
        var instance = new Day10();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public static char FindBendTypeForStart(string[] grid, (int row, int col) pos)
    {
        var northChar = pos.row > 0 ? grid[pos.row - 1][pos.col] : '.';
        var southChar = pos.row < grid.GetLength(0) - 1 ? grid[pos.row + 1][pos.col] : '.';
        var westChar = pos.col > 0 ? grid[pos.row][pos.col - 1] : '.';
        var eastChar = pos.col < grid[pos.row].Length - 1 ? grid[pos.row][pos.col + 1] : '.';

        // Determine the bend type based on the surrounding characters
        if ((southChar == '|' || southChar == 'L' || southChar == 'J') &&
            (eastChar == '-' || eastChar == 'J' || eastChar == '7'))
        {
            return 'F';
        }

        if ((northChar == '|' || northChar == '7' || northChar == 'F') &&
            (eastChar == '-' || eastChar == 'J' || eastChar == '7'))
        {
            return 'L';
        }

        if ((northChar == '|' || northChar == '7' || northChar == 'F') &&
            (westChar == '-' || westChar == 'F' || westChar == 'L'))
        {
            return 'J';
        }

        if ((southChar == '|' || southChar == 'L' || southChar == 'J') &&
            (westChar == '-' || westChar == 'F' || westChar == 'L'))
        {
            return '7';
        }

        throw new InvalidOperationException(
            "Starting position 'S' not found or does not have a surrounding bend type (L, 7, F, J).");
    }

    public override long GetTask1Result(string[] input)
    {
        var startPosition = FindStartPosition(input);

        var loopLength = FindLoopLength(input, startPosition);
        Console.WriteLine($"Loop Length: {loopLength}");

        return loopLength / 2;
    }

    private int FindLoopLength(string[] grid, (int row, int col) start)
    {
        var currentPipe = FindBendTypeForStart(grid, start);
        var prevChar = '|';

        var loopLength = 0;
        var (row, col) = start;
        var prevPos = start;

        do
        {
            loopLength++;

            var dir = _shapeMatrix[currentPipe][prevChar];
            var tmpPos = (row, col);
            switch (dir)
            {
                case Dir.Right:
                    col++;
                    break;
                case Dir.Left:
                    col--;
                    break;
                case Dir.Up:
                    row--;
                    break;
                case Dir.Down:
                    row++;
                    break;
                case Dir.Decide:
                    switch (currentPipe)
                    {
                        case '-':
                            if (prevPos.col > col) { col--; }
                            else { col++; }
                            break;
                        case '|':
                            if (prevPos.row > row) { row--; }
                            else { row++; }
                            break;
                        case 'L':
                            if (prevPos.col > col) { row--; }
                            else { col++; }
                            break;
                        case '7':
                            if (prevPos.col < col) { row++; }
                            else { col--; }
                            break;
                        case 'F':
                            if (prevPos.col > col) { row++; }
                            else { col++; }
                            break;
                        case 'J':
                            if (prevPos.col < col) { row--; }
                            else { col--; }
                            break;
                    }
                    break;
            }

            prevPos = tmpPos;
            prevChar = currentPipe;
            currentPipe = grid[row][col];
        } while (currentPipe != 'S');

        return loopLength;
    }

    public static (int, int) FindStartPosition(string[] grid)
    {
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] == 'S')
                {
                    return (row, col);
                }
            }
        }

        throw new Exception();
    }

    public override long GetTask2Result(string[] input)
    {
        throw new NotImplementedException();
    }
}
