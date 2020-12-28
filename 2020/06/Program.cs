using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace _06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllText("content.txt").Split(Environment.NewLine + Environment.NewLine);
            int sum = 0, sumAll = 0;

            foreach (var item in content)
            {
                char[] working = item.Replace(Environment.NewLine, "").ToCharArray();
                int newLines = Regex.Matches( item , System.Environment.NewLine).Count + 1;
                sum += working.GroupBy(x=>x).ToArray().Count();
                sumAll += working.GroupBy(x=>x).Where(x=>x.Count() == newLines).ToArray().Count();
            }
            Console.WriteLine($"Part 1 - {sum}");
            Console.WriteLine($"Part 2 - {sumAll}");
        }
    }
}
