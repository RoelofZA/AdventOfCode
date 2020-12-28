using System;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
        static string[] Content()
        {
            return File.ReadAllLines("content.txt");
        }

        static void Main(string[] args)
        {

            //Part One
            int floor = 0, cnt = 0;
            char[] content = Content()[0].ToCharArray();

            foreach (char item in content)
            {
                cnt++;
                switch(item)
                {
                    case '(':
                        floor++;
                        break;
                    default:
                        floor--;
                        break;
                }
                if (floor==-1) {
                    Console.WriteLine($"Part 2 - {cnt}");
                }
            }
            Console.WriteLine($"Part 1 - {floor}");
        }
    }
}
