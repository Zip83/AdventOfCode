using System;

namespace AdventOfCode.Y2018
{
    public class Day5
    {
        private const string FILENAME = "2018/inputs/5.txt";
        private const string FILENAME_EXAMPLE = "2018/inputs/5_example.txt";

        private const int FROM = 65; // A
        private const int TO = 90; // Z
        
        public void execute(int taskId)
        {
            switch (taskId)
            {
                case 1:
                    task1();
                    break;
                case 2:
                    task2();
                    break;
            }
        }

        private void task1()
        {
            var line = FileUtil.readLine(FILENAME);
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

        private void task2()
        {
            var input = FileUtil.readLine(FILENAME);
            var min = Int32.MaxValue;
            string line = "";
            for (int ch = FROM; ch <= TO; ch++)
            {
                line = new string(input.ToCharArray());
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