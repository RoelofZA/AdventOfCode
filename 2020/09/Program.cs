using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _09
{
    class Program
    {
        static bool CheckDigit(List<long> codes, long digit) {
            codes = codes.Where(x=>x<digit).ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes.Any(x=>x==digit-codes[i]))
                    return true;
            }            
            return false;
        }

        static bool Part2(List<long> codes, long digit) {
            
            for (int i = 0; i < codes.Count; i++){
                long sum = codes[i];
                for (int j = i+1; j < codes.Count-1; j++)
                {
                    sum+=codes[j];
                    if (sum>digit)
                        break;
                    else if (sum==digit) {
                        codes = codes.GetRange(i, j-i);
                        Console.WriteLine($"Part 2 - {codes.Max()+codes.Min()}");
                        return true;
                    }
                }
            }            
            return false;
        }
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("Content.txt");
            List<long> codes = new List<long>();
            long invalid = 0, invalidPos = 0;

            // Load Content
            foreach (var item in content)
            {
                codes.Add(long.Parse(item));
            }

            for (int i = 25; i < codes.Count; i++)
            {
                if (!CheckDigit(codes.GetRange(i-25, 25), codes[i]))
                {
                    Console.WriteLine($"Part 1 - {codes[i]}");
                    invalid = codes[i];
                    invalidPos = i;
                    break;
                }
            }

            Part2(codes.GetRange(0, (int)invalidPos-1), invalid);
        }
    }
}
