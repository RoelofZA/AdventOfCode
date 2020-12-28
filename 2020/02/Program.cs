using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("content.txt");
            int countPart1 = 0, countPart2 = 0;
            
            foreach (var item in content)
            {
                // Split Line
                string[] line = item.Split(" ");

                // Get Range Check Values
                string[] range = line[0].Split("-");

                //Char Count
                char[] lineChars = line[2].ToCharArray();
                int cnt = lineChars.Count(x=>x == line[1].ToCharArray()[0]);

                // Part 1 - Range Check 
                if (cnt >= int.Parse(range[0]) && cnt <= int.Parse(range[1])) {
                    //Console.WriteLine(item);
                    countPart1++;
                }

                // Part 2 - Range Check
                string first = lineChars[int.Parse(range[0])-1].ToString(), second = lineChars[int.Parse(range[1])-1].ToString();
                if ( first!= second && ( first == line[1].ToCharArray()[0].ToString() || second == line[1].ToCharArray()[0].ToString() )) {
                    //Console.WriteLine(item);
                    countPart2++;
                }
            }
            Console.WriteLine($"Part 1 - {countPart1}");
            Console.WriteLine($"Part 2 - {countPart2}");

        }
    }
}
