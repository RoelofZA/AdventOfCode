using System;
using System.Linq;
using System.IO;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("content.txt");

            int paper = 0;
            int ribbon = 0;

            // Start the Loop
            foreach (var item in content)
            {
                int[] box = item.Split("x").Select(Int32.Parse).ToArray();
                int[] calc = {box[0]*box[1], box[1]*box[2], box[0]*box[2]};
                int small = calc.Min();
                paper+= (calc.Sum() * 2) + small;

                var list = box.OrderBy(x=>x).ToArray();
                int short1 = list[0], short2 = list[1];
                ribbon += (box[0] * box[1] * box[2]) + (2 *short1) + (2 *short2) ;

                Console.WriteLine($"{box[0] * box[1] * box[2]} {2 *short1} {2 *short2} == {box[0]} {box[1]} {box[2]} ");

            }

            Console.WriteLine($"Part 1 - {paper}");
            Console.WriteLine($"Part 2 - {ribbon}");
        }
    }
}
