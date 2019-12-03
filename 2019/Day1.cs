using System;
using System.Data.SqlTypes;
using System.IO;

namespace AdventOfCode._2019
{
    public class Day1 : ADay
    {
        public Day1(bool test) : base(test)
        {
        }

        protected override string FileName => "\\..\\..\\..\\2019\\puzzles\\inputs\\01.txt";
        protected override string FileNameExample => "\\..\\..\\..\\2019\\puzzles\\examples\\01.txt";
        
        protected override void Task1(string fileName)
        {
            double sum = 0;
            var path = Directory.GetCurrentDirectory() + fileName;
            Console.WriteLine(path);
            var inputs = FileUtil.readAllLines(path);
            foreach (var input in inputs)
            {
                int number = int.Parse(input);
                var result = Math.Floor(number / 3.0) - 2;
                sum += result;
            }
            
            Console.WriteLine(sum);
        }

        protected override void Task2(string fileName)
        {
            var path = Directory.GetCurrentDirectory() + fileName;
            Console.WriteLine(path);
            var inputs = FileUtil.readAllLines(path);
            foreach (var input in inputs)
            {
                int number = int.Parse(input);
                var result = Math.Floor(number / 3.0) - 2;
                Console.WriteLine(result);
            }
        }
    }
}