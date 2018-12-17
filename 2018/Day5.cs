using System;

namespace AdventOfCode._2018
{
    public class Day5 : ADay
    {
        private const int From = 65; // A
        private const int To = 90; // Z

        protected override string FileName => "2018/puzzles/inputs/05.txt";
        protected override string FileNameExample => "2018/puzzles/examples/05.txt";
        
        public Day5(bool test) : base(test)
        {
        }

        protected override void Task1(string fileName)
        {
            var line = FileUtil.readLine(fileName);
            var l = line.Length;
            do
            {
                for (int i = 0; i < l - 1; i++)
                {
                    l = line.Length;
                    var c1 = line[i];
                    var c2 = line[i + 1];
                    if (c1 != c2 && c1.ToString().ToUpper() == c2.ToString().ToUpper())
                    {
//                        Console.WriteLine("Removing");
                        line = line.Remove(i, 2);
                        i -= 2;
                        if (i < -1)
                        {
                            i = -1;
                        }
                    }
                }
            } while(l != line.Length);

            Console.WriteLine(line);
            Console.WriteLine(line.Length);
        }

        protected override void Task2(string fileName)
        {
            var input = FileUtil.readLine(fileName);
            var min = Int32.MaxValue;
            for (int ch = From; ch <= To; ch++)
            {
                var line = new string(input.ToCharArray());
                var c = Convert.ToChar(ch);
                Console.WriteLine(c);
                line = line.Replace(c.ToString(), "");
                line = line.Replace(c.ToString().ToLower(), "");
                Console.WriteLine(line);
                var l = input.Length;
                do
                {
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        l = line.Length;
                        var c1 = line[i];
                        var c2 = line[i + 1];
                        if (c1 != c2 && c1.ToString().ToUpper() == c2.ToString().ToUpper())
                        {
//                        Console.WriteLine("Removing");
                            line = line.Remove(i, 2);
                            i -= 2;
                            if (i < -1)
                            {
                                i = -1;
                            }
                        }
                    }
                } while(l != line.Length);

                Console.WriteLine(line);
                if (line.Length < min)
                {
                    min = line.Length;
                }
            }

            Console.WriteLine(min);
        }
    }
}