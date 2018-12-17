using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode._2018
{
    public class Day17 : ADay
    {
        protected override string FileName => "2018/puzzles/inputs/17.txt";
        protected override string FileNameExample => "2018/puzzles/examples/17.txt";

        private static readonly Coords WaterSourceCoords = new Coords(500, 0);

        public Day17(bool test) : base(test)
        {
        }

        protected override void Task1(string fileName)
        {
            string[] inputs = FileUtil.readAllLines(fileName);
            Ground[,] grounds = parseInput(inputs);
            print(grounds);
        }

        protected override void Task2(string fileName)
        {
            throw new System.NotImplementedException();
        }

        private Ground[,] parseInput(string[] inputs)
        {
            var ground = initGround(inputs, out var bounds);
            int xCorrection = bounds.Left - 1;
            ground[WaterSourceCoords.x - xCorrection, WaterSourceCoords.y] = Ground.WaterSpring;
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
                maxX = WaterSourceCoords.x,
                maxY = WaterSourceCoords.y;
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

        private void print(Ground[,] grounds)
        {
            for (int y = 0; y < grounds.GetLength(1); y++)
            {
                for (int x = 0; x < grounds.GetLength(0); x++)
                {
                    Console.Write(((char)grounds[x, y]).ToString());
                }
                Console.WriteLine();
            }
        }

        private struct Coords
        {
            public int x, y;

            public Coords(int p1, int p2)
            {
                x = p1;
                y = p2;
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
    }
}
