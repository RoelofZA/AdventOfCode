using System;
using System.IO;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Part 1 - {Calc(3, 1)}");

            int part2Sum = 1;
            part2Sum *= Calc(1, 1);
            part2Sum *= Calc(3, 1);
            part2Sum *= Calc(5, 1);
            part2Sum *= Calc(7, 1);
            part2Sum *= Calc(1, 2);

            Console.WriteLine($"Part 2 - {part2Sum}");
        }

        static int Calc(int increaseX, int increaseY) {
            string[] content = File.ReadAllLines("content.txt");
            int lineLength = content[0].Length;
            int treeCount = 0;
            int currentX = increaseX;

            for (int i = increaseY; i < content.Length; i += increaseY)
            {
                if (content[i][currentX % lineLength] == '#')
                {
                    treeCount++;
                }
                currentX+=increaseX;
            }
            return treeCount;
        }
    }
}
