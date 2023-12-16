using AdventOfCode.Shared;

namespace AdventOfCode._2019;

public class Day11 : Day
{
    private const char EmptySpace = '.';
    private const char Galaxy = '#';

    public static void Main()
    {
        var instance = new Day11();
        Console.WriteLine();
        Console.WriteLine($"RESULT#1: {instance.GetTask1Result()}");
        Console.WriteLine($"RESULT#2: {instance.GetTask2Result()}");
    }

    public override long GetTask1Result(string[] input)
    {
        return GetTotalPathLength(input);
    }

    public static int GetTotalPathLength(string[] input)
    {
        var expandedUniverse = ExpandUniverse(input);
        var galaxyMap = CreateGalaxyMap(expandedUniverse);

        var numRows = galaxyMap.Length;
        var numCols = galaxyMap[0].Length;
        var totalPathLength = 0;

        for (var i = 0; i < numRows; i++)
        {
            for (var j = 0; j < numCols; j++)
            {
                if (galaxyMap[i][j] != 0)
                {
                    totalPathLength += CalculateShortestPaths(galaxyMap, i, j);
                }
            }
        }

        return totalPathLength / 2;
    }


    public static int CalculateShortestPaths(int[][] galaxyMap, int startX, int startY)
    {
        var numRows = galaxyMap.Length;
        var numCols = galaxyMap[0].Length;

        var distances = new int[numRows, numCols];
        for (var i = 0; i < numRows; i++)
        {
            for (var j = 0; j < numCols; j++)
            {
                distances[i, j] = int.MaxValue;
            }
        }

        distances[startX, startY] = 0;

        var queue = new Queue<(int, int)>();
        queue.Enqueue((startX, startY));

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newX = x + dx;
                var newY = y + dy;

                if (newX >= 0 && newX < numRows && newY >= 0 && newY < numCols)
                {
                    var newDistance = distances[x, y] + 1;
                    if (newDistance < distances[newX, newY])
                    {
                        distances[newX, newY] = newDistance;
                        queue.Enqueue((newX, newY));
                    }
                }
            }
        }

        var totalLength = 0;
        for (var i = 0; i < numRows; i++)
        {
            for (var j = 0; j < numCols; j++)
            {
                if (galaxyMap[i][j] != 0 && (i != startX || j != startY))
                {
                    totalLength += distances[i, j];
                }
            }
        }

        return totalLength;
    }

    public static HashSet<int> GetExpandedRows(string[] input)
    {
        var expandRows = new HashSet<int>();

        var numRows = input.Length;
        for (var row = 0; row < numRows; row++)
        {
            if (input[row].All(c => c == EmptySpace))
            {
                expandRows.Add(row);
            }
        }

        return expandRows;
    }

    public static HashSet<int> GetExpandedCols(string[] input)
    {
        var expandCols = new HashSet<int>();

        var numRows = input.Length;
        var numCols = input[0].Length;
        for (var col = 0; col < numCols; col++)
        {
            var isOnlyEmptySpace = true;
            for (var row = 0; row < numRows; row++)
            {
                if (input[row][col] == Galaxy)
                {
                    isOnlyEmptySpace = false;
                    break;
                }
            }

            if (isOnlyEmptySpace)
            {
                expandCols.Add(col);
            }
        }

        return expandCols;
    }

    public static char[][] ExpandUniverse(string[] input)
    {
        var numRows = input.Length;
        var numCols = input[0].Length;
        var expandRows = GetExpandedRows(input);
        var expandCols = GetExpandedCols(input);

        var newNumberOfRows = numRows + expandRows.Count;
        var newNumberOfCols = numCols + expandCols.Count;

        var numberOfExpandedRows = 0;
        var expandedUniverse = new char[newNumberOfRows][];
        for (var row = 0; row < numRows; row++)
        {
            expandedUniverse[row + numberOfExpandedRows] = new char[newNumberOfCols];

            var numberOfExpandedCols = 0;
            for (var col = 0; col < numCols; col++)
            {
                expandedUniverse[row + numberOfExpandedRows][col + numberOfExpandedCols] = input[row][col];
                if (expandCols.Contains(col))
                {
                    expandedUniverse[row + numberOfExpandedRows][col + numberOfExpandedCols + 1] = input[row][col];
                    numberOfExpandedCols++;
                }
            }

            if (expandRows.Contains(row))
            {
                expandedUniverse[row + numberOfExpandedRows + 1] = expandedUniverse[row + numberOfExpandedRows];
                numberOfExpandedRows++;
            }
        }

        return expandedUniverse;
    }

    public static int[][] CreateGalaxyMap(char[][] universe)
    {
        var numRows = universe.Length;
        var numCols = universe[0].Length;
        var galaxyMap = new int[numRows][];

        var galaxyCount = 1;

        for (var i = 0; i < numRows; i++)
        {
            galaxyMap[i] = new int[numCols];
            for (var j = 0; j < numCols; j++)
            {
                if (universe[i][j] == Galaxy)
                {
                    galaxyMap[i][j] = galaxyCount++;
                }
                else
                {
                    galaxyMap[i][j] = 0;
                }
            }
        }

        return galaxyMap;
    }

    private const int ExpandCoefficient = 1000000;

    public override long GetTask2Result(string[] input)
    {
        return GetTotalPathLength(input, ExpandCoefficient);
    }

    public static long GetTotalPathLength(string[] input, int expandCoefficient)
    {
        var expandedRows = GetExpandedRows(input);
        var expandedCols = GetExpandedCols(input);
        var galaxyMap = CreateGalaxyMap(input);

        var numRows = galaxyMap.Length;
        var numCols = galaxyMap[0].Length;
        var totalPathLength = 0L;

        for (var row = 0; row < numRows; row++)
        {
            for (var col = 0; col < numCols; col++)
            {
                if (galaxyMap[row][col] != 0)
                {
                    totalPathLength += CalculateShortestPaths(galaxyMap, row, col, expandedRows, expandedCols, expandCoefficient);
                }
            }
        }

        return totalPathLength / 2;
    }

    public static int[][] CreateGalaxyMap(string[] universe)
    {
        var numRows = universe.Length;
        var numCols = universe[0].Length;
        var galaxyMap = new int[numRows][];

        var galaxyCount = 1;

        for (var i = 0; i < numRows; i++)
        {
            galaxyMap[i] = new int[numCols];
            for (var j = 0; j < numCols; j++)
            {
                if (universe[i][j] == Galaxy)
                {
                    galaxyMap[i][j] = galaxyCount++;
                }
                else
                {
                    galaxyMap[i][j] = 0;
                }
            }
        }

        return galaxyMap;
    }

    public static long CalculateShortestPaths(int[][] galaxyMap, int startRow, int startCol, HashSet<int> expandedRows,
        HashSet<int> expandedCols, int expandCoefficient)
    {
        var numRows = galaxyMap.Length;
        var numCols = galaxyMap[0].Length;

        var distances = new long[numRows, numCols];
        for (var row = 0; row < numRows; row++)
        {
            for (var col = 0; col < numCols; col++)
            {
                distances[row, col] = long.MaxValue;
            }
        }

        distances[startRow, startCol] = 0;

        var queue = new Queue<(int, int)>();
        queue.Enqueue((startRow, startCol));

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            foreach (var (dRow, dCol) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var newRow = row + dRow;
                var newCol = col + dCol;

                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                {
                    var newDistance = distances[row, col];

                    // Use the appropriate coefficient based on the direction and the presence of expanded rows/columns
                    if (dRow == 1 && expandedRows.Contains(row)) // Moving down and current row is in hashset
                    {
                        newDistance += expandCoefficient;
                    }
                    else if (dRow == -1 && expandedRows.Contains(row - 1)) // Moving up and previous row is in hashset
                    {
                        newDistance += expandCoefficient;
                    }
                    else if (dCol == 1 && expandedCols.Contains(col)) // Moving right and current col is in hashset
                    {
                        newDistance += expandCoefficient;
                    }
                    else if (dCol == -1 && expandedCols.Contains(col - 1)) // Moving left and previous col is in hashset
                    {
                        newDistance += expandCoefficient;
                    }
                    else
                    {
                        newDistance++;
                    }

                    if (newDistance < distances[newRow, newCol])
                    {
                        distances[newRow, newCol] = newDistance;
                        queue.Enqueue((newRow, newCol));
                    }
                }
            }
        }

        var totalLength = 0L;
        for (var i = 0; i < numRows; i++)
        {
            for (var j = 0; j < numCols; j++)
            {
                if (galaxyMap[i][j] != 0 && (i != startRow || j != startCol))
                {
                    totalLength += distances[i, j];
                }
            }
        }

        return totalLength;
    }
}
