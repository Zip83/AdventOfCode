using System;
using System.Collections.Generic;

namespace AdventOfCode._2018

{
    public class Day6 : ADay
    {
        protected override string FileName => "2018/puzzles/inputs/06.txt";
        protected override string FileNameExample => "2018/inputs/examples/06.txt";
        private const char SameDistanceChar = '.';
        
        public Day6(bool test) : base(test)
        {
        }
        
        protected override void Task1(string fileName)
        {
            var matrix = ReadInput(fileName);
            var size = initSize(matrix);

            var candidates = new Dictionary<int, Dictionary<int, char>>();
            for (int i = 0; i < 50; i++)
            {
                foreach (var columns in matrix)
                {
                    foreach (var chars in columns.Value)
                    {
                        tryGrow(columns.Key - 1, chars.Key, chars.Value, matrix, candidates, size);
                        tryGrow(columns.Key, chars.Key - 1, chars.Value, matrix, candidates, size);
                        tryGrow(columns.Key + 1, chars.Key, chars.Value, matrix, candidates, size);
                        tryGrow(columns.Key, chars.Key + 1, chars.Value, matrix, candidates, size);
                    }
                }

                var newCan = new Dictionary<int, Dictionary<int, char>>();
                foreach (var columnsCan in candidates)
                {
                    foreach (var charCan in columnsCan.Value)
                    {
                        if (charCan.Value != SameDistanceChar)
                        {
                            addCharToPos(columnsCan.Key, charCan.Key, charCan.Value, matrix);
                            addCharToPos(columnsCan.Key, charCan.Key, charCan.Value, newCan);
                        }
                    }
                }

                candidates = newCan;
            }
            
            Console.WriteLine("***");
        }

        protected override void Task2(string fileName)
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, Dictionary<int, char>> ReadInput(string file)
        {
            string[] lines = FileUtil.readAllLines(file);
            Dictionary<int, Dictionary<int, char>> matrix = new Dictionary<int, Dictionary<int, char>>();

            int ch = 65;
            foreach (var line in lines)
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

        private Dictionary<char, int> initSize(Dictionary<int, Dictionary<int, char>> matrix)
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

        private bool getChatAtPos(int x, int y, Dictionary<int, Dictionary<int, char>> matrix, out char ch)
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

        private bool addCharToPos(int x, int y, char ch, Dictionary<int, Dictionary<int, char>> matrix)
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
        
        private char replaceCharAtPos(int x, int y, char ch, Dictionary<int, Dictionary<int, char>> matrix)
        {
            char ret = ' ';
            if (matrix.TryGetValue(x, out var columns))
            {
                columns.TryGetValue(y, out ret);
                columns.Remove(y);
                columns.Add(y, ch);
            }

            return ret;
        }

        private void changeSize(Dictionary<char, int> size, char ch, int diff)
        {
            size.TryGetValue(ch, out var s);
            size.Remove(ch);
            size.Add(ch, s + diff);
        }

        private void tryGrow(int x, int y, char ch,
            Dictionary<int, Dictionary<int, char>> matrix,
            Dictionary<int, Dictionary<int, char>> candidates, 
            Dictionary<char, int> size)
        {
            if (getChatAtPos(x, y, matrix, out _))
            {
                return;
            }
            
            if (addCharToPos(x, y, ch, candidates))
            {
                changeSize(size, ch, 1);
            }
            else
            {
                changeSize(size, ch, -1);
                replaceCharAtPos(x, y, SameDistanceChar, candidates);
            }
        }
    }
}