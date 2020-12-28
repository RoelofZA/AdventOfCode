using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace _13
{
    class Program
    {
        static void CW(string value) {
            Console.WriteLine(value);
        }
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");
            long myBus = long.Parse(collection[0]);
            string[] busses = collection[1].Split(",");
            Dictionary<int, int> pattern = new Dictionary<int, int>();

            for (int i = 0; i < busses.Length; i++)
            {
                if (busses[i]!="x") pattern.Add(int.Parse(busses[i]), i);
            }

            pattern = pattern.OrderByDescending(x=>x.Key).ToDictionary(x=>x.Key, x=>x.Value);

            long counter = 0;
            bool found = false, patternFound = false;
            long patternNo = 0;
            long prevSecond = 0;

            while (!found) {
                if (patternFound)
                    counter+=patternNo;
                else
                    counter++;
                
                long calc = pattern.Keys.ToList()[0] * counter;
                long minValue = calc;
                
                for (int i = 1; i < pattern.Keys.Count; i++)
                {
                    //long calc = pattern.Keys.ToList()[0] * counter;
                    long checkVal = calc + (pattern[pattern.Keys.ToList()[i]] - pattern[pattern.Keys.ToList()[0]]); 
                    long checkBus = pattern.Keys.ToList()[i];
                    minValue = Math.Min(minValue, checkVal);
                    if (checkVal % checkBus != 0)
                    {
                        break;
                    }
                    else {
                        if (i == 2 && !patternFound)
                        {
                            if (counter - prevSecond == patternNo)
                            {
                                patternFound = true;
                            }
                            else{
                                patternNo = counter - prevSecond;
                                prevSecond = counter;
                            }
                        }
                        //if (i>4)CW($"{i} - {calc} {checkVal} {checkBus}");
                        if (i == pattern.Count-1){
                            found = true;
                            Console.WriteLine($"Part 2 - {minValue}");
                        }
                    }
                }
            }


            
        }
    }
}
