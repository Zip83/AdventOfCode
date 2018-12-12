using System;
using System.IO;

namespace AdventOfCode
{
    public class FileUtil
    {
        public static string readLine(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            var line = file.ReadLine();  
            Console.WriteLine(line);
            file.Close();

            return line;
        }
        
        public static string[] readAllLines(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}