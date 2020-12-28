using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
            List<int> loadedData = new List<int>();
            string[] content = Content();
            foreach (string val in content)
            {
                int number1 = int.Parse(val);
                int number2 = 2020 - number1;

                if (loadedData.Contains(number2))
                {
                    Console.WriteLine($"Past 1: Numbers {number1} {number2} = {number1 * number2}");
                    //return;
                }
                loadedData.Add(number1);
            }

            for (int i = 1; i < loadedData.Count; i++)
            {
                int number1 = loadedData[i];
                for (int ii = i + 1; ii < loadedData.Count; ii++)
                {
                    int number2 = loadedData[ii];
                    int diff = 2020 - (number1 + number2);
                    if (number1 + number2 < 2020)
                    {
                        if (loadedData.Contains(diff))
                        {
                            Console.WriteLine($"Past 3: Numbers {number1} {number2} {diff} = {number1 * number2 * diff}");
                            //return;
                        }
                    }
                }
            }
        }
    }
}
