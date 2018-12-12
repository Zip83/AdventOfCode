using System;
using System.IO;

namespace AdventOfCode
{
    public class Day5
    {
        private const string FILENAME = "inputs/5_example.txt";
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
            var line = readFile(FILENAME);
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
                        Console.WriteLine("Removing");
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
        }

        private void task2()
        {
            
        }

        private string readFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            var line = file.ReadLine();  
            Console.WriteLine(line);
            file.Close();

            return line;
        }
    }
}