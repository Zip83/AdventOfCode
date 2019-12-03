using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode._2018
{
    public class Day17 : ADay
    {
        protected override string FileName => "2018/puzzles/inputs/17.txt";
        protected override string FileNameExample => "2018/puzzles/examples/17.txt";

        private static Coords WaterSourceCoords = new Coords(500, 0);
        private Ground[,] grounds;
        private Stack<Operation> stack = new Stack<Operation>();

        public Day17(bool test) : base(test)
        {
        }

        protected override void Task1(string fileName)
        {
            string[] inputs = FileUtil.readAllLines(fileName);
            grounds = ParseInput(inputs);
            print();

            GenerateWater();
            var file = File.OpenWrite("2018/puzzles/inputs/17_result.txt");
            for (int y = 0; y < grounds.GetLength(1); y++)
            {
                for (int x = 0; x < grounds.GetLength(0); x++)
                {
                    file.WriteByte((byte)(char)grounds[x, y]);
                }
                file.WriteByte((byte)'\n');
                Console.WriteLine();
            }
            Console.WriteLine();
            Count();
        }

        private void Count()
        {
            var count = 0;
            foreach (var g in grounds)
            {
                if (g == Ground.Reservoir || g == Ground.Passed)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        protected override void Task2(string fileName)
        {
            throw new System.NotImplementedException();
        }

        private Ground[,] ParseInput(string[] inputs)
        {
            var ground = initGround(inputs, out var bounds);
            int xCorrection = bounds.Left - 1;
            WaterSourceCoords.x -= xCorrection;
            ground[WaterSourceCoords.x, WaterSourceCoords.y] = Ground.WaterSpring;
            foreach (var line in inputs)
            {
                var coords = ParseCoords(line);
                foreach (var coord in coords)
                {
                    ground[coord.x - xCorrection, coord.y] = Ground.Clay;
                }

            }

            return ground;
        }

        private void GenerateWater()
        {
            stack.Push(new Operation(Method.FallDown, WaterSourceCoords));
            while (stack.Count > 0)
            {
                var operation = stack.Pop();
                switch (operation.method)
                {
                    case Method.FallDown:
                        FallDown(operation.coord);
                        break;
                    case Method.FillWithPassed:
                        FillWithPassed(operation.coord);
                        break;
                }
                print();
            }
        }

        private void FallDown(Coords coord)
        {
            if (coord.y + 1 >= grounds.GetLength(1) || grounds[coord.x, coord.y + 1] == Ground.Passed)
            {
                return;
            }

            var pos = coord.AddY();
            var ground = grounds[pos.x, pos.y];
            switch (ground)
            {
                case Ground.Passed:
                    stack.Push(new Operation(Method.FallDown, pos));
                    break;
                case Ground.Reservoir:
                    stack.Push(new Operation(Method.FallDown, coord.SubY()));
                    stack.Push(new Operation(Method.FillWithPassed, coord));
                    break;
                case Ground.Sand:
                    grounds[pos.x, pos.y] = Ground.Passed;
                    print();
                    stack.Push(new Operation(Method.FallDown, pos));
                    break;
                case Ground.Clay:
                    stack.Push(new Operation(Method.FallDown, coord.SubY()));
                    stack.Push(new Operation(Method.FillWithPassed, coord));
                    print();
                    break;
            }
        }

        private bool SpreadLeft(Coords coord)
        {
            return Spread(coord, Direction.Left);
        }

        private bool SpreadRight(Coords coord)
        {
            return Spread(coord, Direction.Right);
        }

        private bool Spread(Coords coord, Direction dir)
        {
            var pos = dir == Direction.Left ? coord.SubX() : coord.AddX();
            Ground ground;
            try
            {
                ground = grounds[pos.x, pos.y];
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }

            switch (ground)
            {
                case Ground.Passed:
                case Ground.Reservoir:
                    return Spread(pos, dir);
                case Ground.Sand:
                    var bottom = grounds[coord.x, coord.y + 1];
                    if (bottom == Ground.Sand || bottom == Ground.Passed)
                    {
                        print();
                        stack.Push(new Operation(Method.FallDown, coord));
                        return false;
                    }
                    grounds[pos.x, pos.y] = Ground.Passed;
                    print();
                    return true;
                case Ground.Clay:
                    return false;
                default:
                    return false;
            }
        }

        private void FillWithPassed(Coords coord)
        {
            int spreaded;
            do
            {
                spreaded = 0;
                if (SpreadLeft(coord))
                {
                    spreaded++;
                }
                if (SpreadRight(coord))
                {
                    spreaded++;
                }

                if (spreaded == 0)
                {
                    grounds[coord.x, coord.y] = Ground.Passed;
                }
            } while (spreaded > 0);
            print();
            FillWithReservoir(coord);
        }

        private void FillWithReservoir(Coords coord)
        {
            var clayOnRight = -1;
            var clayOnLeft = -1;
            for(int i = coord.x; i < grounds.GetLength(0); i++)
            {
                if (grounds[i, coord.y] == Ground.Sand)
                {
                    break;
                }
                if (grounds[i, coord.y] == Ground.Clay && grounds[i, coord.y + 1] != Ground.Sand)
                {
                    clayOnRight = i - 1;
                    break;
                }
            }
            for(int i = coord.x; i >= 0; i--)
            {
                if (grounds[i, coord.y] == Ground.Sand)
                {
                    break;
                }
                if (grounds[i, coord.y] == Ground.Clay && grounds[i, coord.y + 1] != Ground.Sand)
                {
                    clayOnLeft = i + 1;
                    break;
                }
            }

            if (clayOnLeft > -1 && clayOnRight > -1)
            {
                for(int i = clayOnLeft; i <= clayOnRight; i++)
                {
                    grounds[i, coord.y] = Ground.Reservoir;
                }
            }
            print();
        }

        private Ground[,] initGround(string[] inputs, out Rectangle bounds)
        {
            bounds = GetBounds(inputs);
            Ground[,] grounds = new Ground[bounds.Width + 2, bounds.Height + 1];
            int xCorrection = bounds.Left - 1;
            for (int x = bounds.Left - 1; x < bounds.Left + bounds.Width + 1; x++)
            {
                for (int y = bounds.Top; y < bounds.Top + bounds.Height + 1; y++)
                {
                    grounds[x - xCorrection, y] = Ground.Sand;
                }
            }

            return grounds;
        }

        private Rectangle GetBounds(string[] inputs)
        {
            int minX = Int32.MaxValue,
                maxX = Int32.MinValue,
                maxY = Int32.MinValue;
            foreach (var line in inputs)
            {
                var coords = ParseCoords(line);
                foreach (var coord in coords)
                {
                    if (coord.x < minX)
                    {
                        minX = coord.x;
                    }
                    if (coord.x > maxX)
                    {
                        maxX = coord.x;
                    }

                    if (coord.y > maxY)
                    {
                        maxY = coord.y;
                    }
                }
            }

            return Rectangle.FromLTRB(minX, WaterSourceCoords.y,maxX + 1, maxY);
        }

        private List<Coords> ParseCoords(string line)
        {
            var coords = line.Split(',');
            var pos1 = coords[0].Split('=');
            var pos2 = coords[1].Split('=');
            var positionsX = ParsePos(pos1[0].Contains('x') ? pos1[1] : pos2[1]);
            var positionsY = ParsePos(pos2[0].Contains('y') ? pos2[1] : pos1[1]);

            List<Coords> result = new List<Coords>();
            foreach (var x in positionsX)
            {
                foreach (var y in positionsY)
                {
                    result.Add(new Coords(x, y));
                }
            }

            return result;
        }

        private int[] ParsePos(string coord)
        {
            int[] result;
            if (coord.Contains('.'))
            {
                var multiPos = coord.Split('.');
                var first = int.Parse(multiPos[0]);
                var last = int.Parse(multiPos[multiPos.Length - 1]);
                var size = last - first + 1;
                result = new int[size];
                for (int i = 0; i < size; i++)
                {
                    result[i] = first + i;
                }
            }
            else
            {
                result = new []{int.Parse(coord)};
            }

            return result;
        }

        private void print()
        {
            return;
            for (int y = 0; y < grounds.GetLength(1); y++)
            {
                for (int x = 0; x < grounds.GetLength(0); x++)
                {
                    Console.Write(((char)grounds[x, y]).ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private struct Coords
        {
            public int x, y;

            public Coords(int p1, int p2)
            {
                x = p1;
                y = p2;
            }

            public Coords AddX()
            {
                return new Coords(x + 1, y);
            }

            public Coords AddY()
            {
                return new Coords(x, y + 1);
            }

            public Coords SubX()
            {
                return new Coords(x - 1, y);
            }

            public Coords SubY()
            {
                return new Coords(x, y - 1);
            }

        }

        private enum Ground
        {
            WaterSpring = '+',
            Sand = '.',
            Clay = '#',
            Reservoir = '~',
            Passed = '|'
        }

        private enum Direction
        {
            Left,
            Right
        }

        private enum Method
        {
            FallDown,
            FillWithPassed
        }

        private struct Operation
        {
            public Method method;
            public Coords coord;

            public Operation(Method method, Coords coords)
            {
                this.method = method;
                this.coord = coords;
            }
        }
    }
}
